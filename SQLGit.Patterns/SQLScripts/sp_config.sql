create procedure sys.sp_configure
    @configname   varchar(35) = null   -- option name to configure
   ,@configvalue  int         = null   -- new configuration value
as
 set nocount on
 
 declare
  @confignum                smallint   --Num of the opt to be configured
    ,@configcount              int   --Num of options like @configname
    ,@show_advance             int   --Y/N Read&Write actions on "advanced" opts
    ,@prevvalue                int
    ,@confignameIn             varchar(35)
      
 select @confignameIn = @configname
     ,@configname = lower(@configname collate Latin1_General_CI_AS)
 
 -- Determine @maxnumber based on advance option in syscurconfigs.
 if (select value_in_use from sys.configurations where configuration_id = 518) = 1
    select @show_advance = 1   -- Display advanced options
 else
    select @show_advance = 0   -- Don't display advanced options
 
 -- If no option name is given, the procedure will just print out all the
 --  options and their values.
 if @configname is NULL
 begin
  select name,
   convert(int, minimum) as minimum,
   convert(int, maximum) as maximum,
   convert(int, isnull(value, value_in_use)) as config_value,
   convert(int, value_in_use) as run_value
  from  sys.configurations
  where (is_advanced = 0 or @show_advance = 1)
  order by lower(name)
 
  return (0)
 end
 
 -- Use @configname and try to find the right option.
 --  If there isn't just one, print appropriate diagnostics and return.
 select @configcount = count(*)
 from sys.configurations
 where lower(name collate Latin1_General_CI_AS) like '%' + @configname + '%'
  and (is_advanced = 0 or @show_advance = 1)
 
 -- If no option, print an error message.
 if @configcount = 0
 begin
  -- If exist but not used in matrix, print MATRIX1_NOT_AVAILABLE  
  select @configcount = count(*)
  from sys.configurations$
  where lower(name collate Latin1_General_CI_AS) like '%' + @configname + '%'
   and (is_advanced = 0 or @show_advance = 1)
  if @configcount <> 0
   begin
    if ServerProperty('IsMatrix') <> 0
     raiserror(28401,-1,-1,@confignameIn)
   end
  else
   raiserror (15123,-1,-1,@confignameIn)
  return (1)
 end
 
 -- If more than one option like @configname, show the duplicates and return.
 if @configcount > 1
 begin
  raiserror (15124,-1,-1,@confignameIn)
  print ' '
 
  select duplicate_options = name
  from sys.configurations
  where lower(name collate Latin1_General_CI_AS) like '%' + @configname + '%'  
   and (is_advanced = 0 or @show_advance = 1)
 
   return (1)
 end
 else
  -- There must be exactly one, so get the full name.
  select @configname = name
  from sys.configurations
  where lower(name collate Latin1_General_CI_AS) like '%' + @configname + '%'
   and (is_advanced = 0 or @show_advance = 1)
 
 -- If @configvalue is NULL, just show the current state of the option.
 if @configvalue is null
 begin
 
  select name,
   convert(int, minimum) as minimum,
   convert(int, maximum) as maximum,
   convert(int, isnull(value, value_in_use)) as config_value,
   convert(int, value_in_use) as run_value
  from sys.configurations
  where (name collate Latin1_General_CI_AS) = @configname
   and (is_advanced = 0 or @show_advance = 1)
 
    return (0)
 end
 
 -- Check Permissions
 if (not has_perms_by_name(NULL,NULL,'alter settings') = 1)
 begin
  raiserror(15247,-1,-1)
  return (1)
 end
 
 -- Now get the configuration number.
 select @confignum = configuration_id, @prevvalue = convert(int, isnull(value, value_in_use))
 from  sys.configurations
 where (@configvalue = 0 or convert(sql_variant, @configvalue) between minimum and maximum)
  and (name collate Latin1_General_CI_AS) = @configname
  and (is_advanced = 0 or @show_advance = 1)
 
 -- If this is the number of default language, we want to make sure
 --  that the new value is a valid language id in Syslanguages.
 if @confignum = 124
 begin
  if not exists (select * from sys.syslanguages
    where langid = @configvalue)
  begin
   -- 0 is default language, us_english
   if @configvalue <> 0
   begin
    raiserror(15127,-1,-1)
    return (1)
   end
  end
 end
 
 -- If this is the number of kernel language, we want to make sure
 --  that the new value is a valid language id in Syslanguages.
 if @confignum = 132
 begin
  if not exists (select * from sys.syslanguages
   where langid = @configvalue)
  begin
   -- 0 is default language, us_english
   if @configvalue <> 0
   begin
      raiserror(15028,-1,-1)
      return (1)
   end
  end
 end
 
 --  "user options" should not try to set incompatible options/values.
 if @confignum = 1534  --"user options"
 begin
  if (@configvalue & (1024+2048) = (1024+2048)) --ansi_null_default_on/off
  begin
   raiserror(15303,-1,-1,@configvalue)
   return (1)
  end
 end
 
 -- Although the @configname is good, @configvalue wasn't in range.
 if @confignum is NULL
 begin
  raiserror(15129,-1,-1,@configvalue,@configname)
  return (1)
 end
 
 -- Now update sysconfigures.
 EXEC %%ServerConfiguration( ConfigID = @confignum ).SetValue( Value = @configvalue )
 
 if @@error <> 0  
 begin
  return (1)
 end
 else
 begin
  declare @configwname nvarchar(35), @configwvalue sql_variant
  select @configwname = @configname,  
   @configwvalue = @configvalue
 
  -- EMDEventType(x_eet_Alter_Instance), EMDUniversalClass(x_eunc_Object), src major id, src minor id, src name
  -- -1 means ignore target stuff, target major id, target minor id, target name,
  -- # of parameters, 5 parameters
  EXEC %%System().FireTrigger(ID = 214, ID = 107, ID = 0, ID = 0, Value = NULL,
   ID = -1, ID = 0, ID = 0, Value = NULL,  
   ID = 2, Value = @configwname, Value = @configwvalue, Value = NULL, Value = NULL, Value = NULL, Value = NULL, Value = NULL)
 
  raiserror(15457,-1,-1, @configname, @prevvalue, @configvalue) with log
  return (0) -- sp_configure
 end