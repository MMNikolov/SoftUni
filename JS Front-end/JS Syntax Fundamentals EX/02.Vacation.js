function solve(number, type, day){
    let totalPrice;
    if(day == 'Friday'){
        if(type == 'Students'){
            if(number >= 30){
                totalPrice = (8.45 * number) * 0.85
            } else {
                totalPrice = 8.45 * number
            }
        } else if(type == 'Business'){
            if(number >= 100){
                totalPrice = (10.90 * number) - (10.90 * 10)
            } else {
                totalPrice = 10.90 * number
            }
        } else if(type == 'Regular'){
            if(number >= 10 && number <= 20){
                totalPrice = (10.90 * number) * 0.95
            } else {
                totalPrice = 10.90 * number
            }
        }
    } else if(day == 'Saturday'){
        if(type == 'Students'){
            if(number >= 30){
                totalPrice = (9.80 * number) * 0.85
            } else {
                totalPrice = 9.80 * number
            }
        } else if(type == 'Business'){
            if(number >= 100){
                totalPrice = (15.60 * number) - (15.60 * 10)
            } else {
                totalPrice = 15.60 * number
            }
        } else if(type == 'Regular'){
            if(number >= 10 && number <= 20){
                totalPrice = (20 * number) * 0.95
            } else {
                totalPrice = 20 * number
            }
        }
    } else if(day == 'Sunday'){
        if(type == 'Students'){
            if(number >= 30){
                totalPrice = (10.46 * number) * 0.85
            } else {
                totalPrice = 10.46 * number
            }
        } else if(type == 'Business'){
            if(number >= 100){
                totalPrice = (16 * number) - (16 * 10)
            } else {
                totalPrice = 16 * number
            }
        } else if(type == 'Regular'){
            if(number >= 10 && number <= 20){
                totalPrice = (22.50 * number) * 0.95
            } else {
                totalPrice = 22.50 * number
            }
        }
    }
    console.log(`Total price: ${totalPrice.toFixed(2)}`);
}   

solve(30, "Students", "Sunday")