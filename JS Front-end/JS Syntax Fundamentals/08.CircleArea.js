function solve(input){
    let result

    if (typeof input === 'string') {
        result = 'We can not calculate the circle area, because we receive a string.'
    } else if(typeof input === 'bool') {
        result = 'We can not calculate the circle area, because we receive a bool.'
    } else{
        num = Number(input)

        result = (num * num * Math.PI).toFixed(2)
    }

    console.log(result);
}

solve(5)
solve('name')