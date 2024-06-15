const apiKey = 'ZtVdh8XQ2U8pWI2gmZ7f796Vh8GllXoN7mr0djNf';
let selectedBrothId = "";
let selectedProteinId = ""

async function fetchAndRenderData(url, elementId) {
    try {
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'x-api-key': apiKey
            }
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json();
        renderCards(data, elementId);
    } catch (error) {
        console.error(`Error fetching data from ${url}:`, error);
    }
}

async function getProteins() {
    await fetchAndRenderData('http://localhost:5010/Products/proteins', 'protein');
}

async function getBroths() {
    await fetchAndRenderData('http://localhost:5010/Products/broths', 'broth');
}

getBroths();
getProteins();
document.querySelector('.containerSuccess').classList.add('hidden');

function renderCards(data, elementId) {
    const container = document.getElementById(elementId);
    container.innerHTML = '';

    data.forEach(item => {
        const card = document.createElement('div');
        card.className = 'card';
        card.dataset.imageInactive = item.imageInactiveDesktop;
        card.dataset.imageActiveDesktop = item.imageActiveDesktop;

        card.innerHTML = `
            <img src="${item.imageInactiveDesktop}" alt="${item.name}" class="cardImg">
            <h3 class="cardTitle">${item.name}</h3>
            <p class="cardDescription">${item.description}</p>
            <p class="cardPrice">Price: $${item.price}</p>
        `;

        card.addEventListener('click', () => {
            handleCardClick(item.id, elementId, card);
        });

        container.appendChild(card);
    });
}

function handleCardClick(itemId, elementId, card) {
    const cards = document.querySelectorAll(`#${elementId} .card`);
    cards.forEach(c => {
        c.classList.remove('selected');
        c.querySelector('.cardTitle').classList.remove('selected');
        c.querySelector('.cardDescription').classList.remove('selected');
        c.querySelector('.cardPrice').classList.remove('selected');
        c.querySelector('img').src = c.dataset.imageInactive;
    });

    card.classList.add('selected');
    card.querySelector('.cardTitle').classList.add('selected');
    card.querySelector('.cardDescription').classList.add('selected');
    card.querySelector('.cardPrice').classList.add('selected');
    card.querySelector('img').src = card.dataset.imageActiveDesktop;

    if (elementId === 'broth') {
        selectedBrothId = itemId;
        enableOrderButton();
    } else if (elementId === 'protein') {
        selectedProteinId = itemId;
    }
}

function enableOrderButton() {
    const orderButton = document.getElementById('placeOrderBtn');
    orderButton.classList.remove('disabled');
    orderButton.classList.add('enabled');
}

async function placeOrder() {
    try {
        const response = await fetch('http://localhost:5010/Products/orders', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'x-api-key': apiKey
            },
            body: JSON.stringify({
                brothId: selectedBrothId,
                proteinId: selectedProteinId
            })
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json();
        return data;

    } catch (error) {
        console.error('Error placing order:', error);
        throw error;
    }
}

async function fetchOrderStatus(orderId) {
    try {
        const response = await fetch(`http://localhost:5010/Products/orders/${orderId}`, {
            method: 'GET',
            headers: {
                'x-api-key': apiKey
            }
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error fetching order status:', error);
        return null;
    }
}

function displayOrderDetails(data) {
    const resultContainer = document.getElementById('resultRamen');
    resultContainer.innerHTML = '';

    const orderDetailsDiv = document.createElement('div');
    orderDetailsDiv.classList.add('orderDetails');

    const orderTitle = document.createElement('h2');
    orderTitle.textContent = 'Your Order:';
    orderTitle.classList.add('TitleDetails');

    const orderImage = document.createElement('img');
    orderImage.src = data.image;
    orderImage.alt = 'Ramen Image';
    orderImage.classList.add('imgDetails');

    const orderDescription = document.createElement('p');
    orderDescription.textContent = data.description;
    orderDescription.classList.add('subTitleDetails');

    orderDetailsDiv.appendChild(orderImage);
    orderDetailsDiv.appendChild(orderTitle);
    orderDetailsDiv.appendChild(orderDescription);

    resultContainer.appendChild(orderDetailsDiv);
}

async function fetchOrderDetails(orderId) {
    try {
        const response = await fetch(`http://localhost:5010/Products/orders/${orderId}`, {
            method: 'GET',
            headers: {
                'x-api-key': apiKey
            }
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error fetching order details:', error);
        return null;
    }
}

document.querySelector('.linkOrderSec2').addEventListener('click', async (event) => {
    event.preventDefault();
    try {
        const data = await placeOrder();
        displayOrderDetails(data);
        document.querySelector('.SectionHidden').classList.add('hidden');
        document.querySelector('.containerSuccess').classList.remove('hidden');
    } catch (error) {
        console.error('Error placing order:', error);
    }
});

document.querySelector('.buttonInformation').addEventListener('click', () => {
    document.querySelector('.containerSuccess').classList.add('hidden');
    document.querySelector('.SectionHidden').classList.remove('hidden');
    location.reload();
});
