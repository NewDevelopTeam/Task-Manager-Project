const elem = document.getElementById("multiBoards");
const value = elem.getAttribute("data-boards");

const boards = JSON.parse(value);

(function (arrOfBoards) {
    const listOfBoards = document.querySelector('.allMultiBoards');

    const objOfBoards = arrOfBoards.reduce((acc, obj) => {
        acc[obj.PositionNo] = obj;
        return acc;
    }, {});

    renderAllBoards(objOfBoards);

    function renderAllBoards(objOfBoards) {
        const fragment = document.createDocumentFragment();

        if (Object.keys(objOfBoards).length == 0) {
            const template = EmptyItemTemplate();
            fragment.appendChild(template);
        }
        else {
            Object.values(objOfBoards).forEach(boardInfo => {
                const board = ListItemTemplate(boardInfo);
                fragment.appendChild(board);
            })
        }

        function EmptyItemTemplate() {
            const item = document.createElement('div');
            item.classList.add("emptyBoard", "isNotDraggable");

            const boardHeader = document.createElement('div');
            boardHeader.classList.add("board-header", "py-4");
            boardHeader.textContent = "У Вас отсутствуют многопользовательские доски";
            item.appendChild(boardHeader);

            return item;
        }

        function ListItemTemplate({ Id, DashboardName } = {}) {
            const boardContainer = document.createElement('div');
            boardContainer.classList.add('Board-container');
            boardContainer.setAttribute('data-id', Id);

            const boardWrapper = document.createElement('div');
            boardWrapper.classList.add('Board-wrapper', 'd-flex', 'flex-column');

            const contentWrapper = document.createElement('div');
            contentWrapper.classList.add('content-wrapper', 'd-flex', 'flex-nowrap', 'justify-content-between', 'align-items-center', 'flex-grow-1', 'p-2', 'mb-3');

            const boardName = document.createElement('div');
            boardName.classList.add('Board-description');
            boardName.textContent = DashboardName;

            const boardEnter = document.createElement('div');
            boardEnter.classList.add('Board-enter', 'multiBoard-link');

            const boardPanel = document.createElement('div');
            boardPanel.classList.add('Board-panel', 'd-flex', 'flex-nowrap', 'justify-content-between', 'align-items-end');

            const boardEditButton = document.createElement('div');
            boardEditButton.classList.add('boardButton', 'button-state', 'button-state__edit', 'hover-slide-up', 'flex-grow-1');

            const boardEditButtonText = document.createElement('div');
            boardEditButtonText.classList.add('boardButton-content');
            boardEditButtonText.textContent = "Редактировать";

            const boardDeleteButton = document.createElement('div');
            boardDeleteButton.classList.add('boardButton', 'multiBoard-delete', 'button-state', 'button-state__delete', 'hover-slide-up', 'flex-grow-1');
            boardDeleteButton.setAttribute("data-toggle", "modal");
            boardDeleteButton.setAttribute('data-target', '#delete-multiBoard');

            const boardDeleteButtonText = document.createElement('div');
            boardDeleteButtonText.classList.add('boardButton-content');
            boardDeleteButtonText.textContent = "Удалить";

            contentWrapper.appendChild(boardName);
            contentWrapper.appendChild(boardEnter);

            boardEditButton.appendChild(boardEditButtonText);
            boardDeleteButton.appendChild(boardDeleteButtonText);

            boardPanel.appendChild(boardEditButton);
            boardPanel.appendChild(boardDeleteButton);

            boardWrapper.appendChild(contentWrapper);
            boardWrapper.appendChild(boardPanel);

            boardContainer.appendChild(boardWrapper);

            return boardContainer;
        }

        listOfBoards.appendChild(fragment);
    }

})(boards);