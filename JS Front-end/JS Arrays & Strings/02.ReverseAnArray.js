function solve(length, numbers) {
    let result = numbers
        .slice(0, length)
        .reverse()
        .join(' ');

    console.log(result);
}

solve(2, [66, 43, 75, 89, 47]);
