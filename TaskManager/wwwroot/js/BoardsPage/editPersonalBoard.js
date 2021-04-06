$(document).ready(function () {
	PersonalBoardHandler();
});
function PersonalBoardHandler(){
	$('.personalBoard-edit').click(function () {
		let idValue = event.target.closest('.Board-container').dataset.id;
		let name = $(event.target).closest('.Board-container').find('.Board-description').text();
		console.log(`idValue: ${idValue}`);
		console.log(`boardName: ${name}`);
		$('.personalBoard-adding__form').val(name);
		$('.edit-personalBoard__button').attr('id', idValue);

		var $submit = $('.edit-personalBoard__button');
		$submit.prop('disabled', true);
		$('.personalBoard-adding__form').on('input change', function () {
			$submit.prop('disabled', !$(this).val().length);
		});
	});
	$('.edit-personalBoard__button').click(function () {
		var boardInfo = {
			BoardId: $('.edit-personalBoard__button').attr('id'),
			BoardName: $('.personalBoard-adding__form').val(),
		};
		console.log(`BoardId: ${boardInfo.BoardId}, BoardName: ${boardInfo.BoardName}`);
		EditPersonalBoard(boardInfo);
		$('#edit-personalBoard').modal('hide');
	})

	$('.btn-close').click(function () {
		$('.board-adding__form').val('');
	})
}
function EditPersonalBoard(boardInfo) {
	$.ajax({
		method: 'POST',
		url: 'https://localhost:44363/boards/editpersonalboard',
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