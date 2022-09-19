app.directive("sidenav", function ($rootScope, $http, $state, $cookies, $localStorage, NgTableParams) {
    return {
        templateUrl: "UI/layouts/common/directives/sidenav.html",
        restrict: "E",
        replace: !0,
        controller: ["$scope", "$state", function (e, $state) {

            e.$state = $state;
            // e.baseMenuName = "";
            console.log($cookies.get('id') + "====" + $cookies.get('roleId'));
            //e.menuItemList = {};
           // alert($localStorage.isMenuLoded);
            if ($localStorage.isMenuLoded == false) {


                $http({
                    method: 'POST',
                    url: 'api/Role/GetPermissionByUserRole',
                    data: { UserId: $cookies.get('id'), Id: $cookies.get('roleId') },
                    headers: { "Content-Type": 'application/json' }
                }).success(function (response) {
                    $rootScope.showLoading = false;
                    console.log(response.obj);
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    } else {
                        var menuItemList = new Array();
                        var submenu = new Array();
                        console.log(response.obj);

                        var myarray = [];
                        returnRoleId = response.obj[0].returnRoleId;
                       // alert(response.obj[0].facultyOrDepId);
                      //  $cookies.put("userRoleToken", response.obj[0].facultyOrDepId);

                        let obj = {};

                        for (var item in response.obj) {
                            if (response.obj[item].IsParent) {
                                // if (response.obj[item].cn >= 1) {
                                //debugger;
                                obj[response.obj[item].RefId] = {
                                    'icon': response.obj[item].Icon,
                                    'displayName': response.obj[item].ParentName,
                                    'state': "javascript:void(0);",
                                    'showSubMenu': false,
                                    'hasSubMenu': false,
                                    'pcoded': false,
                                    'parentActive': false,
                                    'submenu': [],
                                };

                                if (response.obj[item].cn >= 2) {

                                    obj[response.obj[item].RefId].hasSubMenu = true;
                                    obj[response.obj[item].RefId].showSubMenu = true;
                                    // obj[response.obj[item].RefId].state = "#" + response.obj[item].RefId;
                                    ;
                                } else {
                                    //obj[response.obj[item].RefId].state =
                                        //alert("cnt==" + obj[response.obj[item].cn] + "====" + response.obj[item].cn);
                                    obj[response.obj[item].RefId].hasSubMenu = false;
                                    obj[response.obj[item].RefId].showSubMenu = false;
                                    obj[response.obj[item].RefId].state = response.obj[item].PageUrl;
                                }

                            } else if (response.obj[item].IsParent != true) {
                                
                                response.obj[item].icon = response.obj[item].Icon;
                                response.obj[item].displayName = response.obj[item].DisplayName;
                                response.obj[item].state = response.obj[item].PageUrl;
                                obj[response.obj[item].RefId].submenu.push(response.obj[item]);
                            }

                        }
                        e.menuItemList = obj;
                        $localStorage.menulist = e.menuItemList;
                        $localStorage.isMenuLoded = true;
                        console.log(e.menuItemList)

                        $('.modal-backdrop').remove()
                        $state.go('dashboard');
                    }
                }).error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            } else {
                e.menuItemList = $localStorage.menulist;
            }
            e.toggle = function (data) {
                //console.log(data);
               var selindex = "";
                for (var key in $localStorage.menulist) {
                    if ($localStorage.menulist[key].displayName == data.displayName) {
                        
                        if ($localStorage.menulist[key].pcoded == true)
                        {
                            $localStorage.menulist[key].parentActive = false;
                            $localStorage.menulist[key].pcoded = false;
                        }
                        else 
                        {
                            $localStorage.menulist[key].parentActive = true;
                            $localStorage.menulist[key].pcoded = true;
                        }
                        
                    } else {
                        $localStorage.menulist[key].parentActive = false;
                        $localStorage.menulist[key].pcoded = false;

                    }

                }


            }

            if (!$rootScope.reloadPage) {
                $("#pcoded").pcodedmenu({
                    themelayout: 'vertical',
                    verticalMenuplacement: 'left', // value should be left/right
                    verticalMenulayout: 'wide',   // value should be wide/box/widebox
                    MenuTrigger: 'click',
                    SubMenuTrigger: 'click',
                    activeMenuClass: 'active',
                    ThemeBackgroundPattern: 'pattern2',
                    HeaderBackground: 'theme4',
                    LHeaderBackground: 'theme4',
                    NavbarBackground: 'theme4',
                    ActiveItemBackground: 'theme5',
                    SubItemBackground: 'theme2',
                    ActiveItemStyle: 'style0',
                    ItemBorder: true,
                    ItemBorderStyle: 'none',
                    SubItemBorder: true,
                    DropDownIconStyle: 'style3', // Value should be style1,style2,style3
                    FixedNavbarPosition: false,
                    FixedHeaderPosition: false,
                    collapseVerticalLeftHeader: true,
                    VerticalSubMenuItemIconStyle: 'style6',  // value should be style1,style2,style3,style4,style5,style6
                    VerticalNavigationView: 'view1',
                    verticalMenueffect: {
                        desktop: "shrink",
                        tablet: "overlay",
                        phone: "overlay",
                    },
                    defaultVerticalMenu: {
                        desktop: "expanded", // value should be offcanvas/collapsed/expanded/compact/compact-acc/fullpage/ex-popover/sub-expanded
                        tablet: "collapsed", // value should be offcanvas/collapsed/expanded/compact/fullpage/ex-popover/sub-expanded
                        phone: "offcanvas", // value should be offcanvas/collapsed/expanded/compact/fullpage/ex-popover/sub-expanded
                    },
                    onToggleVerticalMenu: {
                        desktop: "collapsed", // value should be offcanvas/collapsed/expanded/compact/fullpage/ex-popover/sub-expanded
                        tablet: "expanded", // value should be offcanvas/collapsed/expanded/compact/fullpage/ex-popover/sub-expanded
                        phone: "expanded", // value should be offcanvas/collapsed/expanded/compact/fullpage/ex-popover/sub-expanded
                    },

                });
            }

        }]
    }
})