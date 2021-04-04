$(document).ready(function () {
	$('.personalBoard-delete').click(function () {
		let idValue = event.target.closest('.Board-container').dataset.id;
		$('.delete-personalBoard__button').attr('id', idValue);
	});
	$('.delete-personalBoard__button').click(function () {
		let id = $('.delete-personalBoard__button').attr('id');
		DeleteBoard(id);
	});
})
function DeleteBoard(idBoard) {
	$.ajax({
		method: 'GET',
		url: 'https://localhost:44363/boards/deletepersonalboard',
		contentType: 'application/json',
		data: $.param({ id: idBoard }, true),
		success: function () {
			window.location.href = "/pages/boards";
		},
		error: function () {
			alert("Произошел сбой");
		}
	});
}