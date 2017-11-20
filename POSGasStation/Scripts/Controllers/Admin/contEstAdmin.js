
angular.module("mainModule").controller("contEstAdmin",["$scope","$http","userService",
  function($scope,$http,userService) {


    $scope.getHttp= function(url , callback){
      $scope.prod;
      var httpObject = $http.get(url);
      httpObject.then(function(promise){
        callback(promise.data);
      }, function(error){ console.log(error);})}

      $scope.postHttp = function(url,data,callback){
        var httpObject = $http.post(url,data);
        httpObject.then(function(promise){
          callback(promise.data);
        }, function(error){ console.log(error);})}



      $scope.TopByCO=function(begin,end){
        var url='http://gsprest.azurewebsites.net/api/Reportes?date1='+begin+'&date2='+end;
        $http.get(url).then(function(msg){
           console.log(msg);     
          }
        );
      }
      $scope.TopByStore=function(store){
        var url='http://gsprest.azurewebsites.net/api/Reportes?suc='+parseInt(store);
        $http.get(url).then(function(msg){
           console.log(msg);     
          }
        );
      }
      $scope.TopByEmp=function(employee){
        var url='http://gsprest.azurewebsites.net/api/Reportes?empl='+parseInt(employee);
        $http.get(url).then(function(msg){
           console.log(msg);     
          }
        );
      }
      $scope.empTime=function(employee){
        var url='http://gsprest.azurewebsites.net/api/Reportes?empl='+(employee);
        $http.get(url).then(function(msg){
           console.log(msg);     
          }
        );
      }
    }]);
    
