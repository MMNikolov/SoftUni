function originalCreate(words) {
   const contentElement = document.getElementById('content');

   const divElements = words
      .map(word => {
         const pElement = document.createElement('p');
         pElement.textContent = word;
         pElement.style.display = 'none';

         const divElement = document.createElement('div');
         divElement.appendChild(pElement);

         divElement.addEventListener('click', () => {
            pElement.style.display = 'block';
         });

         return divElement;
      });

   divElements.forEach(divElement => contentElement.appendChild(divElement));
}
