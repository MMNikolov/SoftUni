function solve(num1, num2, operator){
    return determine(num1, num2, operator)

    function determine(num1, num2, operator){
        let result
        
        switch (operator) {
            case 'multiply':
                result = num1 * num2
            break;
            case 'divide':
                result = num1 / num2
            break;
            case 'add':
                result = num1 + num2
            break;
            case 'subtract':
                result = num1 - num2
            break;
        }
    
        return result
    }
}

console.log(solve(5, 5, 'multiply'));
