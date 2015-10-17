#pragma strict

internal var animator : Animator;
var h : float;
var v : float;

function Start () {

	animator = GetComponent(Animator);

}

function Update () {

	h = Input.GetAxis("Horizontal");
	v = Input.GetAxis("Vertical");

}

function FixedUpdate () {

	animator.SetFloat ("V Input", v);


}