app.directive("specialcategoryforseatmatrixSelect", function () {
    return {
        templateUrl: "UI/components/specialcategoryforseatmatrixSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.activeSpecialCategoryList = [];
            $scope.defaultSpecialCategoryList = [];
            var specialcategoryObj = {
                Id: 0,
                name: "Select Special Category"
            }
            $scope.defaultSpecialCategoryList.push(specialcategoryObj);
            $scope.getActiveSpecialCategoryList = function () {
         
                $http({
                    method: 'POST',
                    url: 'api/Category/getActiveSpecialCategoryList',
                    data: {},
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            alert(response.obj);
                        }
                        else {
                            $scope.tempList = response.obj;
                            $scope.activeSpecialCategoryList = $scope.defaultSpecialCategoryList.concat($scope.tempList);
                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }
            $scope.getActiveSpecialCategoryList();


        }],
        link: function (e, t, a) {
        }
    }
})