app.directive("predefinedSelect", function () {
    return {
        templateUrl: "UI/components/predefinedSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            //$scope.PreDefineListGetActiveList = [];
            //$scope.defaultPreList = [];
            //var preObj = {
            //    Id: 0,
            //    name: "Select Pre - Define"
            //}
            //$scope.defaultPreList.push(preObj);

            //$scope.getPreDefineListGetActiveList = function () {

            //    $http({
            //        method: 'POST',
            //   /*     url: 'api/predefine/PreDefineListGetActiveList',*/
            //        data: {},
            //        headers: { "Content-Type": 'application/json' }
            //    })
            //        .success(function (response) {
            //            $rootScope.showLoading = false;

            //            if (response.response_code != "200") {
            //                alert(response.obj);
            //            }
            //            else {

            //                $scope.tempList = response.obj;
            //                $scope.PreDefineListGetActiveList = $scope.defaultPreList.concat($scope.tempList);

            //            }
            //        })
            //        .error(function (res) {
            //            alert(res.obj);
            //        });
            //}
            //$scope.getPreDefineListGetActiveList();


        }],
        link: function (e, t, a) {
        }
    }
})