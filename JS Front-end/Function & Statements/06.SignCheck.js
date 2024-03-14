function solve(num1, num2, num3){
    let result
    
    const num1Negative = determineIfNumber1IsNegative(num1)
    const num2Negative = determineIfNumber2IsNegative(num2)
    const num3Negative = determineIfNumber3IsNegative(num3)

    if (num1Negative == true && num2Negative == false && num3Negative == false) {
        result = 'Negative'
    } else if (num1Negative == true && num2Negative == true && num3Negative == false) {
        result = 'Positive'
    } else if (num1Negative == true && num2Negative == true && num3Negative == true) {
        result = 'Negative'
    } else if (num1Negative == false && num2Negative == false && num3Negative == false) {
        result = 'Positive'
    } else if (num1Negative == false && num2Negative == true && num3Negative == false) {
        result = 'Negative'
    } else if (num1Negative == false && num2Negative == true && num3Negative == true) {
        result = 'Positive'
    } else if (num1Negative == false && num2Negative == false && num3Negative == true) {
        result = 'Negative'
    }
    
    function determineIfNumber1IsNegative(num1){
        if (num1 < 0) {
            return true;
        }
    
        return false
    }
    
    function determineIfNumber2IsNegative(num2){    
        if (num2 < 0) {
            return true;
        }
    
        return false
    }
    
    function determineIfNumber3IsNegative(num3){    
        if (num3 < 0) {
            return true
        }
    
        return false
    }

    return result
}

console.log(solve(5, 12, -15));

