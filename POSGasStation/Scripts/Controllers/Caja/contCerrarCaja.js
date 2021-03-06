angular.module("mainModule").controller("contCerrarCaja", ["$scope","$http","userService",'$location',
function($scope,$http,userService,$location) {
  $scope.idCash = "";
  $scope.amount="";
  $scope.resumen=[];

  $scope.closeCash=function(){
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

    console.log(this.amount);
    if(userService.getState()){
      var url='http://gsprest.azurewebsites.net/api/Caja';
      var sendData = {
        "idcaja": userService.getCash(),
        "idsucursal": userService.getSucursal(),
        "fecha": today,
        "efectivo": this.amount,
        "tipo":1,
        "idempleado":userService.getUser().cedula
      };
      $scope.postHttp(url,sendData,(data)=>{
       
          userService.setState(false);
          this.amount='';
      
      })
    }
    else{
      alert("This cash register has not been opened");
    }
  };

    $scope.init = function(){
      if(userService.getState()){
        var fecha = new Date();
        var day = fecha.getDate() + "";
        var month = (fecha.getMonth() + 1) + "";
        var year = fecha.getFullYear() + "";
        var hour = fecha.getHours() + "";
        var minutes = fecha.getMinutes() + "";
        var seconds = fecha.getSeconds() + "";
        
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
        fecha=(year + "-" + month + "-" + day);
        var url = 'http://gsprest.azurewebsites.net/api/Ventas?fecha='+fecha+'&idsuc='+userService.getSucursal()+'&idcaja='+userService.getCash();
        $scope.getHttp(url,(data)=>{
          this.resumen=data;
        })
      }


    };


}]);