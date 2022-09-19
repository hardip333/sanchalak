app.directive("socialcategoryforseatmatrixSelect", function () {
    return {
        templateUrl: "UI/components/socialcategoryforseatmatrixSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.activeMstSocialCategoryGetList = [];
            $scope.defaultSocialCategoryList = [];
            var socialcategoryObj = {
                Id: 0,
                SocialCategoryName: "Select Social Category"
            }
            $scope.defaultSocialCategoryList.push(socialcategoryObj);
            $scope.getActiveMstSocialCategoryGetList = function () {
         
                $http({
                    method: 'POST',
                    url: 'api/SocialCategory/MstSocialCategoryGet',
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
                            $scope.activeMstSocialCategoryGetList = $scope.defaultSocialCategoryList.concat($scope.tempList);
                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }
            $scope.getActiveMstSocialCategoryGetList();


        }],
        link: function (e, t, a) {
        }
    }
})