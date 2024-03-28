const inputs = document.querySelectorAll("#checkbox");

const localStorageKey = 'clickedItemIds';
let clickedItemIds = JSON.parse(localStorage.getItem(localStorageKey)) || [];

inputs.forEach(input => {
    input.addEventListener('click', (e) => {
        const itemId = e.target.id;
        const index = clickedItemIds.indexOf(itemId);

        if (index !== -1) {
            clickedItemIds.splice(index, 1);
        } else {
            clickedItemIds.push(itemId);
        }

        localStorage.setItem(localStorageKey, JSON.stringify(clickedItemIds));

        
        fetch('/Shop/Filter', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(filterData)
        })
    });
});

const filterData = {
    categoryIds: clickedItemIds, // Örnek kategori kimlikleri
    brandIds: [4, 5, 6], // Örnek marka kimlikleri
    fromPrice: 100, // Örnek fiyat aralığı
    toPrice: 500,
    IsInStock: true // Stokta olan ürünleri filtrele
};
