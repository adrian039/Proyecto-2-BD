angular.module("mainModule").controller("contAbrirCaja", ["$scope","$http","userService",'$location',
function($scope,$http,userService,$location) {
  $scope.idCash = "";
  $scope.amount="";
  $scope.cajaList;

  $scope.openCash=function(){
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth()+1; //January is 0!
    
    var today = new Date();
    var day = today.getDate() + "";
    var month = (today.getMonth() + 1) + "";
    var year = today.getFullYear() + "";
    var hour = today.getHours() + "";
    var minutes = today.getMinutes() + "";
    var seconds = today.getSeconds() + "";
    
    day = checkZero(day);
    month = checkZero(month);
    year = checkZero(year);
    hour = checkZero(hour);
    mintues = checkZero(minutes);
    seconds = checkZero(seconds);

    function checkZero(data){
      if(data.length == 1){
        data = "0" + data;
      }
      return data;
    } 
    
    today=(year + "-" + month + "-" + day + " " + hour + ":" + minutes + ":" + seconds);
    console.log(today);
    if(!userService.getState()){
    var url='http://gsprest.azurewebsites.net/api/Caja';
    var sendData = {
      "idcaja": this.idCash,
      "idsucursal": userService.getSucursal(),
      "fecha": today,
      "efectivo": this.amount,
      "tipo":0,
      "idempleado":userService.getUser().cedula
    };
    $scope.postHttp(url,sendData,(data)=>{
      if(data){
        userService.setCash(this.idCash);
        userService.setState(true);
        this.amount="";
        this.idCash="";
      }
      else{
        alert("Error, check the information")        
      }
    })
  }else{
    alert("The ragister cash is already open");
  }

  };

  $scope.init = function(){
    var url='http://gsprest.azurewebsites.net/api/Sucursales?idSucursal='+userService.getSucursal();
    $scope.getHttp(url,(data)=>{
      this.cajaList=data;
    })
  };


}]);