$(document).ready(function () {
	$('.card-delete').click(function () {
		let idValue = event.target.id;
		$('.delete-card__button').attr('id', idValue);
	});

	$('.delete-card__button').click(function () {
		let idCard = $('.delete-card__button').attr('id');
		DeleteCard(idCard);
	});
})

function DeleteCard(idCard) {
	$.ajax({
		method: 'POST',
		url: 'https://localhost:44363/cards/deleteCard',
		contentType: 'application/json',
		data: JSON.stringify(idCard),
		success: function () {
			window.location.href = "/pages/cards";
		},
		error: function () {
			alert("Произошел сбой");
		}
	});
}
