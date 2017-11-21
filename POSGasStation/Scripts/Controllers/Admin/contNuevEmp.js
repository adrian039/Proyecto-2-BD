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
    $scope.idRol;
    $scope.idStore;

    $scope.sucList;
    
    $scope.init = function(){
      var url='http://gsprest.azurewebsites.net/api/Roles';
      $scope.getHttp(url,(data)=>{
        $scope.rolList=data;
        console.log(data);
      });

      url='http://gsprest.azurewebsites.net/api/Sucursales?idEmpresa='+userService.getCompany();
      $scope.getHttp(url,(data)=>{
        this.sucList=data;
      });

    };

    $scope.setRol=function(role){
      $scope.idRol=role;
      
    };

    $scope.setStore=function(store){
      $scope.idStore=store;
    };



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
                "idrol":this.idRol,
                "idsucursal":this.idStore,
                "estado":1
              }
              console.log("Employee: "+sendData.idrol+sendData.idsucursal);
              $scope.postHttp(url,sendData,(data)=>{
                console.log("Data: "+data);
              });
            }
            else {
              alert("Error(03): Passwords not match");
            }
      
        };
    }]);