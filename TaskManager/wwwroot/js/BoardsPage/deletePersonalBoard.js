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
		type: 'DELETE',
		url: 'https://localhost:44363/boards/deletepersonalboard?' + $.param({ id: idBoard }),
		contentType: 'application/json', 
		success: function () {
			window.location.href = "/pages/boards";
		},
		error: function () {
			alert("Произошел сбой");
		}
	});
}