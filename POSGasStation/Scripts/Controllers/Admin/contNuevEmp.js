angular.module("mainModule").controller("contNuevEmp", ["$scope","$http","directionService",'storeService','$location','userService',

  function($scope,$http,directionService,storeService,$location,userService) {
    $scope.cedula;
    $scope.nombre;
    $scope.pApellido;
    $scope.sApellido;
    $scope.username;
    $scope.password;
    $scope.conPassword;
    $scope.email;
    

      $scope.createEmployee = function () {
        if (this.password == this.conPassword) {
                var url = "http://gsprest.azurewebsites.net/api/Empleados";
                var sendData = {
                  "cedula": parseInt(this.cedula),
                  "nombre": this.nombre,
                  "papellido": this.pApellido,
                  "sapellido":this.sApellido,
                  "username": this.username,
                  "password":this.password,
                  "email":this.email,
                  "estado":1
                }
               $scope.postHttp(url,sendData,(data)=>{
                  console.log("Data: "+data);
                });
              }
              else {
                alert("Error(03): Passwords not match");
              }
        
          };
      }]);