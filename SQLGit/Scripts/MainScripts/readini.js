const ini = require('ini');
const reader = new FileReader();
const fs = require('fs')

async function readFiles(file){
    try {
        const data = await  fs.readFile(file);
        console.log(data.toString());
    }catch (e){
        console.log(e.message)
    }
}
$("#Btn").click(function () {
    alert("dsfgfdh");
})