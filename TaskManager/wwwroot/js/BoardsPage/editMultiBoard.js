$(document).ready(function () {
	MultiBoardHandler();
});

function MultiBoardHandler() {
	$('.multiBoard-edit').click(function () {
		let idValue = event.target.closest('.Board-container').dataset.id;
		console.log(idValue);
		let boardName = $(event.target).closest('.Board-container').find('.Board-description').text();
		$('.board-adding__form').val(boardName);
		$('.edit-multiBoard__button').attr('id', idValue);

		var $submit = $('.edit-multiBoard__button');
		$submit.prop('disabled', true);
		$('.board-adding__form').on('input change', function () {
			$submit.prop('disabled', !$(this).val().length);
		});
	});
	$('.board-adding__form').click(function () {
		const id = $('.edit-multiBoard__button').attr('id');
		const name = $('.board-adding__form').val();
		EditMultiBoard(id, name);
		$('#edit-multiBoard__button').modal('hide');
	})

	$('.btn-close').click(function () {
		$('.board-adding__form').val('');
	})
}

function EditMultiBoard(id, name) {
	$.ajax({
		method: 'GET',
		url: 'https://localhost:44363/boards/editmultiboard',
		contentType: 'application/json',
		data: $.param({ boardId: id, boardName: name }, true),
		success: function (data) {
			ShowMultiBoards(data.jsonBoards);
		},
		error: function () {
			console.log("Error");
		}
	});
}
