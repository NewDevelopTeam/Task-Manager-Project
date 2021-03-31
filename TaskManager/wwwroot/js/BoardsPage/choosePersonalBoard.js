$('.personalBoard-link').click(function (e) {
	const linkOfBoard = e.target;
	const board = linkOfBoard.closest('.Board-container');
	const boardId = board.getAttribute('data-id');
	console.log(boardId);

	window.location.href = "/Boards/PersonalBoard?boardid=" + boardId;
});
