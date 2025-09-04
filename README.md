Tutorial crack getcurse: https://www.youtube.com/watch?v=NlQifFLG2x8

Альтернатива через js в странице браузера
Alternative via js in browser page

```js
(function () {
	const originalOpen = XMLHttpRequest.prototype.open;

	XMLHttpRequest.prototype.open = function (method, url) {
		this.addEventListener('load', function () {
			var listTarget = [];

			try // get resultHash
			{
				const json = JSON.parse(this.responseText);

				if (json.data && json.data.resultHash) {
					console.log('resultHash:', json.data.resultHash);

					const decodedString = atob(json.data.resultHash);
					const dataObject = JSON.parse(decodedString);

					console.log(dataObject);
					const variants = dataObject.question.variants;

					var i = 0;

					variants.forEach(variant => {
						if (variant.is_right === 1) {
							i++;
							listTarget.unshift(variant.value);

							console.log(variant.value);
						}
					});
				}
			} catch (e) {
				//console.log('Eroor get json');
			}


			setTimeout(() => {
				//console.log('Start color painter');

				//const targetClass = 'btn btn-default btn-mark-variant js__btn-variant';
				var elements = document.querySelectorAll('button, input[type="button"]');
				var curentTarget = '';

				for (var i = 0; i < listTarget.length; i++) {
					curentTarget = 'data-value="' + listTarget[i] + '"';
					console.log(curentTarget);

					for (var j = 0; j < elements.length; j++) {
						var element = elements[j];

						//console.log('Element:', element.outerHTML);

						//data-value="[&quot;x=kπ, где k∈Z&quot;]"
						//data-value="x=kπ, где k∈Z"

						var value = element.getAttribute('value');
						var classes = element.getAttribute('class') || '';

						if (element.outerHTML.includes(curentTarget)) {

							//console.log('AAAAAAAAAAAAAAAAAAA');
							//console.log('Class:', classes);
							//console.log('Value:', value);

							element.style.cssText = `
							background-color: #83c7a4 !important;
							color: #2b0327;
							border: 6px solid #267500;
							padding: 5px 10px !important;
							border-radius: 4px !important;
							box-shadow: 0 2px 4px rgba(0,0,0,0.2) !important;
							font-weight: bold;
							cursor: pointer;`;

							element.click();
						}
						//console.log('Good color painter');
					}
				}
				try { // input
					for (var j = 0; j < elements.length; j++) {
						var element = elements[j];
						console.log(element.outerHTML);
						curentTarget = 'class="btn btn-primary btn-send-all-variants btn-send-variant'
						if (element.outerHTML.includes(curentTarget)) {
							element.click();
						}
					}
				} catch {
					setTimeout(() => {
						for (var j = 0; j < elements.length; j++) {
							var element = elements[j];
							console.log(element.outerHTML);
							curentTarget = 'class="btn btn-primary btn-send-all-variants btn-send-variant'
							if (element.outerHTML.includes(curentTarget)) {
								element.click();
							}
						}
					}, 10);
				}
			}, 10);
		});
		return originalOpen.apply(this, arguments);
	};
})();
```
