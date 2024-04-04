function search() {
   const townNamesElement = document.getElementById('towns');
   const searchTextElement = document.getElementById('searchText');
   const resultElement = document.getElementById('result');
   const townElements = Array.from(townNamesElement.children)
   const searchText = searchTextElement.value

   let matches = 0;

   for (const townElement of townElements) {
      if (townElement.textContent.toLowerCase().includes(searchText.toLowerCase())) {
         townElement.style.textDecoration = 'underline';
         townElement.style.fontWeight = 'bold';
         matches++;
      }
   }

   resultElement.textContent = `${matches} matches found`
}
