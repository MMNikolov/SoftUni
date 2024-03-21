function solve(input){
    for (const line in input) {
        console.log(`Name: ${input[line]} -- Personal Number: ${input[line].length}`);
    }
}

solve([
    'Silas Butler',    
    'Adnaan Buckley',   
    'Juan Peterson',    
    'Brendan Villarreal'
])