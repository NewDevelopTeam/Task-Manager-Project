$(document).ready(function () {
	MultiBoardHandler();
});
function MultiBoardHandler() {
	$('.multiBoard-edit').click(function () {
		let idValue = event.target.closest('.Board-container').dataset.id;
		let name = $(event.target).closest('.Board-container').find('.Board-description').text();
		console.log(`idValue: ${idValue}`);
		console.log(`boardName: ${name}`);
		$('.multiBoard-adding__form').val(name);
		$('.edit-multiBoard__button').attr('id', idValue);

		var $submit = $('.edit-multiBoard__button');
		$submit.prop('disabled', true);
		$('.multiBoard-adding__form').on('input change', function () {
			$submit.prop('disabled', !$(this).val().length);
		});
	});
	$('.edit-multiBoard__button').click(function () {
		var boardInfo = {
			BoardId: $('.edit-multiBoard__button').attr('id'),
			BoardName: $('.multiBoard-adding__form').val(),
		};
		console.log(`BoardId: ${boardInfo.BoardId}, BoardName: ${boardInfo.BoardName}`);
		EditMultiBoard(boardInfo);
		$('#edit-multiBoard').modal('hide');
	})

	$('.btn-close').click(function () {
		$('.board-adding__form').val('');
	})
}
function EditMultiBoard(boardInfo) {
	$.ajax({
		method: 'POST',
		url: 'https://localhost:44363/boards/editmultiboard',
		contentType: 'application/json',
		data: JSON.stringify(boardInfo),
		success: function (data) {
			window.location.href = "/pages/boards";
		},
		error: function () {
			console.log("Error");
		}
	});
}