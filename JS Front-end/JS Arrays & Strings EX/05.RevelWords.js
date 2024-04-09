function solve(wordsInput, template){
    const words = wordsInput.split(', ');
    let startIndex = -1;
    let endIndex = -1;

    for (let i = 0; i < template.length; i++) {
        if (template[i] === '*') {
            if (startIndex < 0) {
                startIndex = i;
                endIndex = i + 1;
            } else{
                endIndex = i + 1;
            }
        } else{
            if (startIndex >= 0) {
                let length = endIndex - startIndex;
                let word = words.find(w => w.length === length);
                template = template.replace('*'.repeat(length), word)

                startIndex = -1;
                endIndex = -1;
            }
        }
        
    }

    if (startIndex >= 0) {
        let length = endIndex - startIndex;
        let word = words.find(w => w.length === length);
        template = template.replace('*'.repeat(length), word)
    }

    console.log(template);
}

solve('great, learning', 'softuni is ***** place for ******** new programming languages')