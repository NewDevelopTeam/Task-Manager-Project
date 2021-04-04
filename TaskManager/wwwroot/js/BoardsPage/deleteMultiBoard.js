$(document).ready(function () {
	$('.multiBoard-delete').click(function () {
		let idValue = event.target.closest('.Board-container').dataset.id;
		$('.delete-multiBoard__button').attr('id', idValue);
	});
	$('.delete-multiBoard__button').click(function () {
		let id = $('.delete-multiBoard__button').attr('id');
		DeleteBoard(id);
	});
})
function DeleteBoard(idBoard) {
	$.ajax({
		method: 'GET',
		url: 'https://localhost:44363/boards/deletemultiboard',
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