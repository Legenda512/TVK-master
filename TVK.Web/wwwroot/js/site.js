	var total_pics_num = 100000; // колличество изображений
    var interval = 1500;    // задержка между изображениями
    var time_out = 1500;       // задержка смены изображений
    var i = 0;
    var timeout;
    var flag = false;

    function fade_to_next() {
        i = i + 1;
        var image_next = 'recived/screenshot-' + i + '.jpeg';
        var ava = document.getElementById('image');
        ava.setAttribute('src', image_next);
        timeout = setTimeout("fade_to_next()", time_out);
    }

    function run() {
		var data = new FormData();
		data.append( "Command",  document.getElementById("command").value );
		data.append( "Address",  document.getElementById("address").value );
		fetch(window.location.href, {
			method: 'POST',
			headers: {
			'Accept': 'application/json',
			//'Content-Type': 'application/json'
			},
			body: data
			});

        if (document.getElementById("command").value === "stop" || flag === true ) {
            clearTimeout(timeout);
            flag = false;
        } else {
            timeout = setTimeout(fade_to_next, interval);
            flag = true;

        }
    }
	
	document.getElementById("click").addEventListener('click', run);
	console.log("heelo");
	
