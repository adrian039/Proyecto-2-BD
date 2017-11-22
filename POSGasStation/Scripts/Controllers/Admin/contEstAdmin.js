
angular.module("mainModule").controller("contEstAdmin",["$window","$location","$sce","$scope","$http","userService",
  function($window,$location,$sce,$scope,$http,userService) {


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
        var url='http://localhost:58706/api/Reportes?date1='+begin+'&date2='+end; 
          $http.get(url, {  responseType: 'arraybuffer' })
          .then(function (response) {                  
            var file = new Blob([response.data], { type: 'application/pdf' });
             var fileURL = URL.createObjectURL(file);
             $window.open($sce.trustAsResourceUrl(fileURL));
          }
        );
      }
      $scope.TopByStore=function(store){
        var url='http://localhost:58706/api/Reportes?suc='+parseInt(store);
        $http.get(url, {  responseType: 'arraybuffer' })
        .then(function (response) {                  
          var file = new Blob([response.data], { type: 'application/pdf' });
           var fileURL = URL.createObjectURL(file);
           $window.open($sce.trustAsResourceUrl(fileURL));   
          }
        );
      }
      $scope.TopByEmp=function(employee){
        var url='http://localhost:58706/api/Reportes?empl='+parseInt(employee);
        $http.get(url, {  responseType: 'arraybuffer' })
        .then(function (response) {                  
          var file = new Blob([response.data], { type: 'application/pdf' });
           var fileURL = URL.createObjectURL(file);
           $window.open($sce.trustAsResourceUrl(fileURL));   
          }
        );
      }
      $scope.empTime=function(date){
        var url='http://localhost:58706/api/Reportes?date='+date;
        $http.get(url, {  responseType: 'arraybuffer' })
        .then(function (response) {                  
          var file = new Blob([response.data], { type: 'application/pdf' });
           var fileURL = URL.createObjectURL(file);
           $window.open($sce.trustAsResourceUrl(fileURL));   
          }
        );
      }
      $scope.lowStock=function(){
        var url='http://localhost:58706/api/Reportes';
        $http.get(url, {  responseType: 'arraybuffer' })
        .then(function (response) {                  
          var file = new Blob([response.data], { type: 'application/pdf' });
           var fileURL = URL.createObjectURL(file);
           $window.open($sce.trustAsResourceUrl(fileURL));   
          }
        );
      }
      $scope.getSalesByDate=function(date){
        var url='http://localhost:58706/api/Reportes?fecha='+date;
        $http.get(url, {  responseType: 'arraybuffer' })
        .then(function (response) {                  
          var file = new Blob([response.data], { type: 'application/pdf' });
           var fileURL = URL.createObjectURL(file);
           $window.open($sce.trustAsResourceUrl(fileURL));   
          }
        );
      }
    }]);
    
