angular.module("mainModule").controller("contSucAdmin", ["$scope","$http","$location"
,"userService","directionService",
function($scope,$http,$location,userService,directionService) {
  $scope.sucList;
  
      $scope.init = function(){
        var url='http://gsprest.azurewebsites.net/api/Sucursales?idEmpresa='+userService.getCompany();
        $scope.getHttp(url,(data)=>{
          this.sucList=data;
        })

        
      };

    

      $scope.delete=function(id){
       
      
    }


  $scope.edit=function(ID,dir,nme){


  }

}]);
