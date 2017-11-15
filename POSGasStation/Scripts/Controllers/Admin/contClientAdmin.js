angular.module("mainModule").controller("contClientAdmin",["$scope","$http",
function($scope,$http) {
  $scope.clientList;


      $scope.init = function(){
        var url='http://gsprest.azurewebsites.net/api/Clientes';
        $scope.getHttp(url,(data)=>{
          this.clientList=data;
        })        

      };


      $scope.edit=function(id,dir,nme,pAp,sAp,pass,usr,ste,pnz,date){

        var url = 'http://'+getIp()+':58706/api/Clientes';
        var data={
          "Cedula":id,
          "Direccion":dir,
          "Nombre":nme,
          "pApellido":pAp,
          "sApellido":sAp,
          "Password":pass,
          "Username":usr,
          "Estado":ste,
          "Penalizacion":pnz,
          "Nacimiento":date

        };
        $http.put(url,data)
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


      $scope.delete=function(id,dir,nme,pAP,sAp,pass,usr,ste,pnz,data){

        var url = 'http://'+getIp()+':58706/api/Clientes';
        var data={
          "Cedula":id,
          "Direccion":dir,
          "Nombre":nme,
          "pApellido":pAp,
          "sApellido":sAp,
          "Password":pass,
          "Username":usr,
          "Estado":ste,
          "Penalizacion":pnz,
          "Nacimiento":date

        };
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



