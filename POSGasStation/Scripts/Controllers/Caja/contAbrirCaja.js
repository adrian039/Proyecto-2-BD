angular.module("mainModule").controller("contAbrirCaja", ["$scope","$http","userService",'$location',
function($scope,$http,userService,$location) {
  $scope.idCash = "";
  $scope.amount="";

  $scope.openCash=function(){
    var url='';
    $http.post(url).then(function (msg) {
      if (msg.data){
       
      }
      else{
        alert("Error opening cash register");
      }
     });
  };

  $scope.init = function(){  
    alert("Sucursal "+userService.getSucursal());
    var url='http://gsprest.azurewebsites.net/api/Empleados?cedula='+cedulaEmp;
    $scope.getHttp(url,(data)=>{
      $scope.options=data;
      $('#myModal').modal({ show: false});
      $('#myModal').modal("show");
      userService.setEmpActive();
      
    })
  };


}]);