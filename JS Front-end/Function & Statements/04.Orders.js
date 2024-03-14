function solve(product, number){
    function determine(product){
    
        let price
        
        switch (product) {
            case 'coffee':
                price = 1.50;
                break
            case 'water':
                price = 1;
                break
            case 'coke':
                price = 1.40;
                break
            case 'snacks':
                price = 2;
                break
            default:
                break;
        }
    
        return price
    }

    return (determine(product) * number).toFixed(2)
}

console.log(solve("water", 5));

