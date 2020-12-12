const ini = require('ini');
const reader = new FileReader();
const fs = require('fs')

function readFiles(file){
    var data = ini.parse(fs.readFileSync(file, 'utf-8'));
    
    console.log(data);
    
    
}