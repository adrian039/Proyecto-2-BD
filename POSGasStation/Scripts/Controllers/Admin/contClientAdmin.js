angular.module("mainModule").controller("contClientAdmin",["$scope","$http",
function($scope,$http) {
  $scope.clientList;

  $scope.cedula;
  $scope.nombre;
  $scope.pApellido;
  $scope.sApellido;
  

      $scope.init = function(){
        var url='http://gsprest.azurewebsites.net/api/Clientes';
        $scope.getHttp(url,(data)=>{
          this.clientList=data;
        })        

      };


      $scope.edit=function(){
        console.log("Cedula "+this.cedula);
        var url = 'http://gsprest.azurewebsites.net/api/Clientes/'+this.cedula;
        var sendData = {
          "cedula": parseInt(this.cedula),
          "nombre": this.nombre,
          "papellido": this.pApellido,
          "sapellido":this.sApellido,
          "estado":1
        };
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

        var url = 'http://gsprest.azurewebsites.net/api/Clientes/'+id;
        
        $http.delete(url,data)
        .then(
            function(response){
              // success callback
              console.log("erase");
              animation();

            }, 
            function(response){
              // failure callback
            }
         );
      }

}]);



