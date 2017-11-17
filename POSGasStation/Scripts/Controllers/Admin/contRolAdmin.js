angular.module("mainModule").controller("contRolAdmin", ["$scope","$http",'userService',
function($scope,$http,userService) {
  $scope.rolList;
  

      $scope.init = function(){
        var url='http://gsprest.azurewebsites.net/api/Roles';
        $scope.getHttp(url,(data)=>{
          this.rolList=data;
        });

      };

      $scope.updateRol = function(id,desc,name){
        var url='http://gsprest.azurewebsites.net/api/Roles';
        var send={
          "idrol": id,
          "nombre": name,
          "descripcion": desc,
          "estado": 1
        }
        $http.put(url,send)
        .then(
            function(response){
              // success callback
              console.log("update");
              $scope.init();
            }, 
            function(response){
              // failure callback
            }
         );
      };

      $scope.deleteRol=function(id){
        console.log("id rol: "+id);
        var url='http://gsprest.azurewebsites.net/api/Roles?idRol='+id;
        $http.delete(url)
        .then(
            function(response){
              // success callback
              alert("Rol Deleted");
              $scope.init();
            }, 
            function(response){
              // failure callback
            }
         );
      }
    }]);