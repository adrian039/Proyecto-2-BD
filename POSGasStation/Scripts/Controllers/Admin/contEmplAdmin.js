angular.module("mainModule").controller("contEmplAdmin",["$scope","$http","userService",
function($scope,$http,userService) {
  $scope.employeelist;

  $scope.cedula;
  $scope.nombre;
  $scope.pApellido;
  $scope.sApellido;
  $scope.username;
  $scope.password;
  $scope.email;


      $scope.init = function(){
        var url='http://gsprest.azurewebsites.net/api/Empleados?idEmpresa='+userService.getCompany();
        $scope.getHttp(url,(data)=>{
          this.employeelist=data;
        })

      };


      $scope.edit=function(){
        var url = "http://gsprest.azurewebsites.net/api/Empleados/"+this.cedula;
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
        $http.put(url,sendData)
        .then(
          function(response){
              // success callback
              $scope.init();
            }, 
            function(response){
              // failure callback
            }
            );
      }

      $scope.delete=function(id){

        var url = 'http://gsprest.azurewebsites.net/api/Empleados/'+id;
        
        $http.delete(url)
        .then(
            function(response){
              // success callback
              console.log("Se elimino");
              $scope.init();
            }, 
            function(response){
              // failure callback
            }
         );
        
      }
    }]);