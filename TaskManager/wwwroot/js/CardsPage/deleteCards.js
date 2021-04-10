

import { EmptyItemTemplate } from './showCards.js';

const listOfCards = document.querySelector('.listofCards');
const deleteForm = document.getElementById('delete-card');
const deleteFormConfirmation = deleteForm.querySelector('.delete-card__button');

listOfCards.addEventListener('click', PassIdCardtoForm);
deleteFormConfirmation.addEventListener('click', SubmitToDeleteCard);

function PassIdCardtoForm(event) {
	const clickedElement = event.target;
	if (clickedElement.classList.contains('card-delete')) {
		const cardId = clickedElement.id;
		deleteFormConfirmation.setAttribute('id', cardId);
	};	
}
function SubmitToDeleteCard(event) {
	const cardId = event.target.id;
	DeleteCard(cardId);
	$('#delete-card').modal('hide');
}
function DeleteCard(idCard) {
	var request = new XMLHttpRequest();
	let url = new URL('https://localhost:44363/cards/deletecard');
	url.searchParams.set('id', idCard);
	request.open('GET', url, true);
	request.setRequestHeader('Content-Type', 'application/json');
	request.onload = function () {
		if (this.status >= 200 && this.status < 400) {
			RemoveCardFromDOM(idCard);
		} else {
			alert("error");
		}
	}
	request.send(null);
}
function RemoveCardFromDOM(cardId) {
	const arrofItems = Array.from(listOfCards.children);
	arrofItems.forEach(card => {
		if (card.dataset.id == cardId) {
			card.remove();
        }
	})
	if (listOfCards.children.length === 0) {
		listOfCards.appendChild(EmptyItemTemplate());
    }
}