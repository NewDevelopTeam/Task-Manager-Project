$(document).ready(function () {

	$('.card-edit').click(function () {
		
		let idValue = event.target.id;
		let textCard = $(this).closest('li.card-personal').find('.card-description').text();

		$('.card-adding__form').val(textCard);
		$('.edit-card__button').attr('id', idValue);
	});

	var $submit = $('.edit-card__button');
	$submit.prop('disabled', true);
	$('.card-adding__form').on('input change', function () {
		$submit.prop('disabled', !$(this).val().length);
	});
	
	$('.edit-card__button').click(function () {
		var cardInfo = {
			CardId: $('.edit-card__button').attr('id'),
			CardText: $('.card-adding__form').val(),
		};
		EditCard(cardInfo);
	});
})

function EditCard(cardInfo) {
	$.ajax({
		method: 'POST',
		url: 'https://localhost:44363/cards/editCards',
		contentType: 'application/json',
		data: JSON.stringify(cardInfo),
		success: function () {
		window.location.href = "/pages/cards";
		},
		error: function () {
			alert("Произошел сбой");
		}
	});
}
