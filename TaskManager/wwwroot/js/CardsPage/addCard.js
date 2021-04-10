
console.log("cards работает")
const addForm = document.getElementById('add-card');
const textArea = addForm.querySelector('.card-adding__form');
const addCardButton = addForm.querySelector('.add-card__button');

addCardButton.addEventListener('click', function () {
    console.log("нажатие на кнопку");



})


textArea.addEventListener('input', function (event) {
    console.log("внутри");
    if (textArea.classList)
})