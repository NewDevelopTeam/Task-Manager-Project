

import '../preloader.js';
import { ShowPersonalCards } from './showCards.js';
import './deleteCards.js';
import './editCards.js';
import './sortCards.js';


ShowPersonalCards();

//function ready() {
//	if (document.readyState != 'loading') {
//		console.log("вызов showpersonalcards")
//		ShowPersonalCards();		
//	} else {
//		console.log("отложен вызов");
//		document.addEventListener('DOMContentLoaded', ShowPersonalCards);	
//	}
//}
//ready();