function solve(number) {
    const isEven = x => x % 2 === 0;
    const isOdd = x => !isEven(x);
    
    const evenSum = calculateDigitSum(number, isEven);
    const oddSum = calculateDigitSum(number, isOdd);

    printResult(oddSum, evenSum);

    function printResult(oddSum, evenSum) {
        console.log(`Odd sum = ${oddSum}, Even sum = ${evenSum}`);
    }

    function calculateDigitSum(number, filter) {
        const filteredDigits = number
            .toString()
            .split('')
            .map(Number)
            .filter(filter);
    
        const sum = filteredDigits.reduce((acc, digit) => acc + digit, 0);
    
        return sum;
    }
}

solve(1000435);