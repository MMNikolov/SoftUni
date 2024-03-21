function solve(input){
    for (const line in input) {
        const [town, latitude, longitude] = line.split(" | ")

        console.log(`{ town: '${town}', latitude: '${latitude.toFixed(2)}', longitude: '${longitude}' }`);
    }
}

solve(['Sofia | 42.696552 | 23.32601',
     'Beijing | 39.913818 | 116.363625'])