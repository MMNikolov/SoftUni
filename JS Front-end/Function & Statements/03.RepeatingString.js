function solve(text, number){
    let result = '';

    for (let i = 0; i < number; i++) {
        result += text
    }

    return result
}

console.log(solve("abc", 3));
