

const listOfCards = document.querySelector('.listofCards');
const editForm = document.getElementById('edit-card');
const editFormConfirmation = editForm.querySelector('.edit-card__button');

listOfCards.addEventListener('click', PassCardInfoToForm);
editFormConfirmation.addEventListener('click', ChangeCardInfo);

function PassCardInfoToForm(event) {
	editFormConfirmation.disabled = true;
	const clickedElement = event.target;
	if (clickedElement.classList.contains('card-edit')) {
		const cardId = clickedElement.id;
		editFormConfirmation.setAttribute('id', cardId);
		const cardText = clickedElement.closest('.card-wrapper').querySelector('.card-description').textContent;

		const textAreaForm = editForm.querySelector('.card-adding__form');
		textAreaForm.value = cardText;
		console.log(textAreaForm);
	
		$("#edit-card").on('shown.bs.modal', function () {
			textAreaForm.focus();
		});

		textAreaForm.addEventListener('input', CheckTextAreaForm);

		function CheckTextAreaForm() {
			if (textAreaForm.value != '') {
				editFormConfirmation.disabled = false;
			} else {
				editFormConfirmation.disabled = true;
			}
		}
	}
}
function ChangeCardInfo(event) {
	const id = editFormConfirmation.id;
	const description = editForm.querySelector('.card-adding__form').value;
	EditCard(id, description);
	$('#edit-card').modal('hide');
}
function EditCard(idCard, textCard) {
	var request = new XMLHttpRequest();
	let url = new URL('https://localhost:44363/cards/editcard');
	url.searchParams.set('id', idCard);
	url.searchParams.set('description', textCard);
	request.open('GET', url, true);
	request.setRequestHeader('Content-Type', 'application/json');
	request.onload = function () {
		if (this.status >= 200 && this.status < 400) {
			UpdateCardInDOM(idCard, textCard);
		} else {
			alert("error");
		}
	}
	request.send(null);
}
function UpdateCardInDOM(id, description) {
	const arrofItems = Array.from(listOfCards.children);
	arrofItems.forEach(card => {
		if (card.dataset.id == id) {
			card.querySelector('.card-description').textContent = description;
        }
    })
}