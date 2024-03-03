function solve(year){
    if(year == 1900){
        console.log("no");
    } else if(year % 4 == 0){
        console.log("yes");
    } else{
        console.log("no");
    }
}

solve(2003)