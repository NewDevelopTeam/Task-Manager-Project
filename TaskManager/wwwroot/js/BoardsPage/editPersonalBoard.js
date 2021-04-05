$(document).ready(function () {
	PersonalBoardHandler();
});

function PersonalBoardHandler(){
	$('.personalBoard-edit').click(function () {
		console.log("ksdjf")
		let idValue = event.target.closest('.Board-container').dataset.id;
		console.log(idValue);
		let boardName = $(event.target).closest('.Board-container').find('.Board-description').text();
		$('.board-adding__form').val(boardName);
		$('.edit-personalBoard__button').attr('id', idValue);

		var $submit = $('.edit-personalBoard__button');
		$submit.prop('disabled', true);
		$('.board-adding__form').on('input change', function () {
			$submit.prop('disabled', !$(this).val().length);
		});
	});

	$('.board-adding__form').click(function () {
		const id = $('.edit-personalBoard__button').attr('id');
		const name = $('.board-adding__form').val();
		EditPersonalBoard(id, name);

		$('#edit-personalBoard__button').modal('hide');
	})

	$('.btn-close').click(function () {
		$('.board-adding__form').val('');
	})
}

function EditPersonalBoard(id, name) {
	$.ajax({
		method: 'GET',
		url: 'https://localhost:44363/boards/editpersonalboard',
		contentType: 'application/json',
		data: $.param({ boardId: id, boardName: name }, true),
		success: function (data) {
			ShowPersonalBoards(data.jsonBoards);
		},
		error: function () {
			console.log("Error");
		}
	});
}