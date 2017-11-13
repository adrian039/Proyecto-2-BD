angular.module("mainModule").service('clientService', function () {
    var name;
    var pAp;
    var sAp;
    var ID;

    this.getName=function(){
        return name;
    }
    this.setName=function(nme){
        name=nme;
    }

    this.getpAp=function(){
        return pAp;
    }
    this.setpAp=function(fSur){
        pAp=fSur;
    }

    this.getsAp=function(){
        return sAp;
    }
    this.setsAp=function(sSur){
        sAp=sSur;
    }

    this.getID=function(){
        return ID;
    }
    this.setID=function(ced){
        Id=ced;
    }

	this.reset = function () {
		name = null;
		pAp= null;
		sAp= null;
		ID = null;
    }
    
    this.setDefaultClient=function(){
        name = "Cliente";
		pAp= "Cliente";
		sAp= "Cliente";
		ID = 0;
    }
  });