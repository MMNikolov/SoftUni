function solve(a, b) {
    const result = calculateFactorial(a) / calculateFactorial(b)

    console.log(result.toFixed(2));

    function calculateFactorial(number) {
        if (number <= 1) {
            return 1;
        }
    
        return number * calculateFactorial(number - 1)
    }
}

solve(5, 2);