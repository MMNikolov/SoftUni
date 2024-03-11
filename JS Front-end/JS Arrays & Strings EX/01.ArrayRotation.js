function solve(array, number){
    for (let i = 0; i < number; i++) {
        array.push(array.shift())
    }

    console.log(array.join(' '));
}

solve([51, 47, 32, 61, 21], 2)

