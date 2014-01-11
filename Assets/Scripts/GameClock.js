#pragma strict

var totalTime : float = 0.0;
var h : int;
var m : int;
var s : int;
var hr;
var min;
var sec;

function Update () {
	totalTime += Time.deltaTime;
	
	h = totalTime / 3600;
	m = totalTime / 60;
	s = totalTime % 60;
	
	hr = ((h < 10) ? '0' : '') + h.ToString();
	min = ((m < 10) ? '0' : '') + m.ToString();
	sec = ((s < 10) ? '0' : '') + s.ToString();
	         
	GetComponent(TextMesh).text = hr + ':' + min + ':' + sec;
}