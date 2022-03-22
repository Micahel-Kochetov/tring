��
wC:\Users\Климнюк\tring\App\Assets\Plugins\Zenject\Source\Editor\SceneParentLoading\SceneParentAutomaticLoader.cs
	namespace		 	
Zenject		
 
.		 
Internal		 
{

 
[ 
InitializeOnLoad 
] 
public 

static 
class &
SceneParentAutomaticLoader 2
{ 
static &
SceneParentAutomaticLoader )
() *
)* +
{ 	
EditorApplication 
.  
playModeStateChanged 2
+=3 5"
OnPlayModeStateChanged6 L
;L M
} 	
static 
void "
OnPlayModeStateChanged *
(* +
PlayModeStateChange+ >
state? D
)D E
{ 	
if 
( 
state 
== 
PlayModeStateChange ,
., -
ExitingEditMode- <
)< =
{ 
try 
{ =
1ValidateMultiSceneSetupAndLoadDefaultSceneParents E
(E F
)F G
;G H
} 
catch 
( 
	Exception  
e! "
)" #
{ 
EditorApplication %
.% &
	isPlaying& /
=0 1
false2 7
;7 8
throw 
new 
ZenjectException .
(. /
$str b
,b c
ed e
)e f
;f g
}   
}!! 
else"" 
if"" 
("" 
state"" 
=="" 
PlayModeStateChange"" 1
.""1 2
EnteredEditMode""2 A
)""A B
{## 
}'' 
}(( 	
public** 
static** 
void** =
1ValidateMultiSceneSetupAndLoadDefaultSceneParents** L
(**L M
)**M N
{++ 	
var,, 
defaultContractsMap,, #
=,,$ %#
LoadDefaultContractsMap,,& =
(,,= >
),,> ?
;,,? @
var11 

sceneInfos11 
=11 &
GetLoadedZenjectSceneInfos11 7
(117 8
)118 9
;119 :
var22 
contractMap22 
=22 '
GetCurrentSceneContractsMap22 9
(229 :

sceneInfos22: D
)22D E
;22E F
foreach44 
(44 
var44 
	sceneInfo44 "
in44# %

sceneInfos44& 0
)440 1
{55 
ProcessScene66 
(66 
	sceneInfo66 &
,66& '
contractMap66( 3
,663 4
defaultContractsMap665 H
)66H I
;66I J
}77 
}88 	
static:: 

Dictionary:: 
<:: 
string::  
,::  !
LoadedSceneInfo::" 1
>::1 2'
GetCurrentSceneContractsMap::3 N
(::N O
List;; 
<;; 
LoadedSceneInfo;;  
>;;  !

sceneInfos;;" ,
);;, -
{<< 	
var== 
contractMap== 
=== 
new== !

Dictionary==" ,
<==, -
string==- 3
,==3 4
LoadedSceneInfo==5 D
>==D E
(==E F
)==F G
;==G H
foreach?? 
(?? 
var?? 
info?? 
in??  

sceneInfos??! +
)??+ ,
{@@ 
AddToContractMapAA  
(AA  !
contractMapAA! ,
,AA, -
infoAA. 2
)AA2 3
;AA3 4
}BB 
returnDD 
contractMapDD 
;DD 
}EE 	
staticGG 
voidGG 
ProcessSceneGG  
(GG  !
LoadedSceneInfoHH 
	sceneInfoHH %
,HH% &

DictionaryII 
<II 
stringII 
,II 
LoadedSceneInfoII .
>II. /
contractMapII0 ;
,II; <

DictionaryJJ 
<JJ 
stringJJ 
,JJ 
stringJJ %
>JJ% &
defaultContractsMapJJ' :
)JJ: ;
{KK 	
ifLL 
(LL 
	sceneInfoLL 
.LL 
SceneContextLL &
!=LL' )
nullLL* .
)LL. /
{MM 
AssertNN 
.NN 
IsNullNN 
(NN 
	sceneInfoNN '
.NN' (
DecoratorContextNN( 8
)NN8 9
;NN9 :
ProcessSceneParentsOO #
(OO# $
	sceneInfoOO$ -
,OO- .
contractMapOO/ :
,OO: ;
defaultContractsMapOO< O
)OOO P
;OOP Q
}PP 
elseQQ 
{RR 
AssertSS 
.SS 
	IsNotNullSS  
(SS  !
	sceneInfoSS! *
.SS* +
DecoratorContextSS+ ;
)SS; <
;SS< ="
ProcessSceneDecoratorsTT &
(TT& '
	sceneInfoTT' 0
,TT0 1
contractMapTT2 =
,TT= >
defaultContractsMapTT? R
)TTR S
;TTS T
}UU 
}VV 	
staticXX 
voidXX "
ProcessSceneDecoratorsXX *
(XX* +
LoadedSceneInfoYY 
	sceneInfoYY %
,YY% &

DictionaryZZ 
<ZZ 
stringZZ 
,ZZ 
LoadedSceneInfoZZ .
>ZZ. /
contractMapZZ0 ;
,ZZ; <

Dictionary[[ 
<[[ 
string[[ 
,[[ 
string[[ %
>[[% &
defaultContractsMap[[' :
)[[: ;
{\\ 	
var]] !
decoratedContractName]] %
=]]& '
	sceneInfo]]( 1
.]]1 2
DecoratorContext]]2 B
.]]B C!
DecoratedContractName]]C X
;]]X Y
LoadedSceneInfo__ 
decoratedSceneInfo__ .
;__. /
ifaa 
(aa 
contractMapaa 
.aa 
TryGetValueaa '
(aa' (!
decoratedContractNameaa( =
,aa= >
outaa? B
decoratedSceneInfoaaC U
)aaU V
)aaV W
{bb '
ValidateDecoratedSceneMatchcc +
(cc+ ,
	sceneInfocc, 5
,cc5 6
decoratedSceneInfocc7 I
)ccI J
;ccJ K
returndd 
;dd 
}ee 
decoratedSceneInfogg 
=gg  '
LoadDefaultSceneForContractgg! <
(gg< =
	sceneInfohh 
,hh !
decoratedContractNamehh 0
,hh0 1
defaultContractsMaphh2 E
)hhE F
;hhF G
EditorSceneManagerjj 
.jj 
MoveSceneAfterjj -
(jj- .
decoratedSceneInfojj. @
.jj@ A
ScenejjA F
,jjF G
	sceneInfojjH Q
.jjQ R
ScenejjR W
)jjW X
;jjX Y'
ValidateDecoratedSceneMatchll '
(ll' (
	sceneInfoll( 1
,ll1 2
decoratedSceneInfoll3 E
)llE F
;llF G
ProcessScenenn 
(nn 
decoratedSceneInfonn +
,nn+ ,
contractMapnn- 8
,nn8 9
defaultContractsMapnn: M
)nnM N
;nnN O
}oo 	
staticqq 
voidqq 
ProcessSceneParentsqq '
(qq' (
LoadedSceneInforr 
	sceneInforr %
,rr% &

Dictionaryss 
<ss 
stringss 
,ss 
LoadedSceneInfoss .
>ss. /
contractMapss0 ;
,ss; <

Dictionarytt 
<tt 
stringtt 
,tt 
stringtt %
>tt% &
defaultContractsMaptt' :
)tt: ;
{uu 	
foreachvv 
(vv 
varvv 
parentContractNamevv +
invv, .
	sceneInfovv/ 8
.vv8 9
SceneContextvv9 E
.vvE F
ParentContractNamesvvF Y
)vvY Z
{ww 
LoadedSceneInfoxx 

parentInfoxx  *
;xx* +
ifzz 
(zz 
contractMapzz 
.zz  
TryGetValuezz  +
(zz+ ,
parentContractNamezz, >
,zz> ?
outzz@ C

parentInfozzD N
)zzN O
)zzO P
{{{ $
ValidateParentChildMatch|| ,
(||, -

parentInfo||- 7
,||7 8
	sceneInfo||9 B
)||B C
;||C D
continue}} 
;}} 
}~~ 

parentInfo
�� 
=
�� )
LoadDefaultSceneForContract
�� 8
(
��8 9
	sceneInfo
��9 B
,
��B C 
parentContractName
��D V
,
��V W!
defaultContractsMap
��X k
)
��k l
;
��l m
AddToContractMap
��  
(
��  !
contractMap
��! ,
,
��, -

parentInfo
��. 8
)
��8 9
;
��9 : 
EditorSceneManager
�� "
.
��" #
MoveSceneBefore
��# 2
(
��2 3

parentInfo
��3 =
.
��= >
Scene
��> C
,
��C D
	sceneInfo
��E N
.
��N O
Scene
��O T
)
��T U
;
��U V&
ValidateParentChildMatch
�� (
(
��( )

parentInfo
��) 3
,
��3 4
	sceneInfo
��5 >
)
��> ?
;
��? @
ProcessScene
�� 
(
�� 

parentInfo
�� '
,
��' (
contractMap
��) 4
,
��4 5!
defaultContractsMap
��6 I
)
��I J
;
��J K
}
�� 
}
�� 	
static
�� 
LoadedSceneInfo
�� )
LoadDefaultSceneForContract
�� :
(
��: ;
LoadedSceneInfo
�� 
	sceneInfo
�� %
,
��% &
string
��' -
contractName
��. :
,
��: ;

Dictionary
��< F
<
��F G
string
��G M
,
��M N
string
��O U
>
��U V!
defaultContractsMap
��W j
)
��j k
{
�� 	
string
�� 
	scenePath
�� 
;
�� 
if
�� 
(
�� 
!
�� !
defaultContractsMap
�� $
.
��$ %
TryGetValue
��% 0
(
��0 1
contractName
��1 =
,
��= >
out
��? B
	scenePath
��C L
)
��L M
)
��M N
{
�� 
throw
�� 
Assert
�� 
.
�� 
CreateException
�� ,
(
��, -
$str�� �
.
�� 
Fmt
�� 
(
�� 
contractName
�� %
,
��% &
	sceneInfo
��' 0
.
��0 1
Scene
��1 6
.
��6 7
name
��7 ;
)
��; <
)
��< =
;
��= >
}
�� 
Scene
�� 
scene
�� 
;
�� 
try
�� 
{
�� 
scene
�� 
=
��  
EditorSceneManager
�� *
.
��* +
	OpenScene
��+ 4
(
��4 5
	scenePath
��5 >
,
��> ?
OpenSceneMode
��@ M
.
��M N
Additive
��N V
)
��V W
;
��W X
}
�� 
catch
�� 
(
�� 
	Exception
�� 
e
�� 
)
�� 
{
�� 
throw
�� 
new
�� 
ZenjectException
�� *
(
��* +
$str
�� N
.
��N O
Fmt
��O R
(
��R S
	sceneInfo
��S \
.
��\ ]
Scene
��] b
.
��b c
name
��c g
)
��g h
,
��h i
e
��j k
)
��k l
;
��l m
}
�� 
return
�� #
CreateLoadedSceneInfo
�� (
(
��( )
scene
��) .
)
��. /
;
��/ 0
}
�� 	
static
�� 
void
�� )
ValidateDecoratedSceneMatch
�� /
(
��/ 0
LoadedSceneInfo
�� 
decoratorInfo
�� )
,
��) *
LoadedSceneInfo
��+ :
decoratedInfo
��; H
)
��H I
{
�� 	
var
�� 
decoratorIndex
�� 
=
��  
GetSceneIndex
��! .
(
��. /
decoratorInfo
��/ <
.
��< =
Scene
��= B
)
��B C
;
��C D
var
�� 
decoratedIndex
�� 
=
��  
GetSceneIndex
��! .
(
��. /
decoratedInfo
��/ <
.
��< =
Scene
��= B
)
��B C
;
��C D
var
�� 
activeIndex
�� 
=
�� 
GetSceneIndex
�� +
(
��+ , 
EditorSceneManager
��, >
.
��> ?
GetActiveScene
��? M
(
��M N
)
��N O
)
��O P
;
��P Q
Assert
�� 
.
�� 
That
�� 
(
�� 
decoratorIndex
�� &
<
��' (
decoratedIndex
��) 7
,
��7 8
$str�� �
,��� �
decoratorInfo
�� 
.
�� 
Scene
�� #
.
��# $
name
��$ (
,
��( )
decoratedInfo
��* 7
.
��7 8
Scene
��8 =
.
��= >
name
��> B
)
��B C
;
��C D
if
�� 
(
�� 
activeIndex
�� 
>
�� 
decoratorIndex
�� ,
)
��, -
{
��  
EditorSceneManager
�� "
.
��" #
SetActiveScene
��# 1
(
��1 2
decoratorInfo
��2 ?
.
��? @
Scene
��@ E
)
��E F
;
��F G
}
�� 
}
�� 	
static
�� 
void
�� &
ValidateParentChildMatch
�� ,
(
��, -
LoadedSceneInfo
�� 
parentSceneInfo
�� +
,
��+ ,
LoadedSceneInfo
��- <
	sceneInfo
��= F
)
��F G
{
�� 	
var
�� 
parentIndex
�� 
=
�� 
GetSceneIndex
�� +
(
��+ ,
parentSceneInfo
��, ;
.
��; <
Scene
��< A
)
��A B
;
��B C
var
�� 

childIndex
�� 
=
�� 
GetSceneIndex
�� *
(
��* +
	sceneInfo
��+ 4
.
��4 5
Scene
��5 :
)
��: ;
;
��; <
var
�� 
activeIndex
�� 
=
�� 
GetSceneIndex
�� +
(
��+ , 
EditorSceneManager
��, >
.
��> ?
GetActiveScene
��? M
(
��M N
)
��N O
)
��O P
;
��P Q
Assert
�� 
.
�� 
That
�� 
(
�� 
parentIndex
�� #
<
��$ %

childIndex
��& 0
,
��0 1
$str�� �
,��� �
parentSceneInfo��� �
.��� �
Scene��� �
.��� �
name��� �
,��� �
	sceneInfo��� �
.��� �
Scene��� �
.��� �
name��� �
)��� �
;��� �
if
�� 
(
�� 
activeIndex
�� 
>
�� 
parentIndex
�� )
)
��) *
{
��  
EditorSceneManager
�� "
.
��" #
SetActiveScene
��# 1
(
��1 2
parentSceneInfo
��2 A
.
��A B
Scene
��B G
)
��G H
;
��H I
}
�� 
}
�� 	
static
�� 
int
�� 
GetSceneIndex
��  
(
��  !
Scene
��! &
scene
��' ,
)
��, -
{
�� 	
for
�� 
(
�� 
int
�� 
i
�� 
=
�� 
$num
�� 
;
�� 
i
�� 
<
��  
EditorSceneManager
��  2
.
��2 3

sceneCount
��3 =
;
��= >
i
��? @
++
��@ B
)
��B C
{
�� 
if
�� 
(
��  
EditorSceneManager
�� &
.
��& '

GetSceneAt
��' 1
(
��1 2
i
��2 3
)
��3 4
==
��5 7
scene
��8 =
)
��= >
{
�� 
return
�� 
i
�� 
;
�� 
}
�� 
}
�� 
throw
�� 
Assert
�� 
.
�� 
CreateException
�� (
(
��( )
)
��) *
;
��* +
}
�� 	
static
�� 

Dictionary
�� 
<
�� 
string
��  
,
��  !
string
��" (
>
��( )%
LoadDefaultContractsMap
��* A
(
��A B
)
��B C
{
�� 	
var
�� 
configs
�� 
=
�� 
	Resources
�� #
.
��# $
LoadAll
��$ +
<
��+ ,(
DefaultSceneContractConfig
��, F
>
��F G
(
��G H(
DefaultSceneContractConfig
��H b
.
��b c
ResourcePath
��c o
)
��o p
;
��p q
var
�� 
map
�� 
=
�� 
new
�� 

Dictionary
�� $
<
��$ %
string
��% +
,
��+ ,
string
��- 3
>
��3 4
(
��4 5
)
��5 6
;
��6 7
foreach
�� 
(
�� 
var
�� 
config
�� 
in
��  "
configs
��# *
)
��* +
{
�� 
foreach
�� 
(
�� 
var
�� 
info
�� !
in
��" $
config
��% +
.
��+ ,
DefaultContracts
��, <
)
��< =
{
�� 
if
�� 
(
�� 
info
�� 
.
�� 
ContractName
�� )
.
��) *
Trim
��* .
(
��. /
)
��/ 0
.
��0 1
IsEmpty
��1 8
(
��8 9
)
��9 :
)
��: ;
{
�� 
Log
�� 
.
�� 
Warn
��  
(
��  !
$str
��! k
,
��k l
AssetDatabase
��m z
.
��z {
GetAssetPath��{ �
(��� �
config��� �
)��� �
)��� �
;��� �
continue
��  
;
��  !
}
�� 
Assert
�� 
.
�� 
That
�� 
(
��  
!
��  !
map
��! $
.
��$ %
ContainsKey
��% 0
(
��0 1
info
��1 5
.
��5 6
ContractName
��6 B
)
��B C
,
��C D
$str�� �
,��� �
info��� �
.��� �
ContractName��� �
,��� �
AssetDatabase��� �
.��� �
GetAssetPath��� �
(��� �
config��� �
)��� �
)��� �
;��� �
map
�� 
.
�� 
Add
�� 
(
�� 
info
��  
.
��  !
ContractName
��! -
,
��- .
AssetDatabase
��/ <
.
��< =
GetAssetPath
��= I
(
��I J
info
��J N
.
��N O
Scene
��O T
)
��T U
)
��U V
;
��V W
}
�� 
}
�� 
return
�� 
map
�� 
;
�� 
}
�� 	
static
�� 
LoadedSceneInfo
�� #
CreateLoadedSceneInfo
�� 4
(
��4 5
Scene
��5 :
scene
��; @
)
��@ A
{
�� 	
var
�� 
info
�� 
=
�� &
TryCreateLoadedSceneInfo
�� /
(
��/ 0
scene
��0 5
)
��5 6
;
��6 7
Assert
�� 
.
�� 
	IsNotNull
�� 
(
�� 
info
�� !
,
��! "
$str
��# O
,
��O P
scene
��Q V
.
��V W
name
��W [
)
��[ \
;
��\ ]
return
�� 
info
�� 
;
�� 
}
�� 	
static
�� 
LoadedSceneInfo
�� &
TryCreateLoadedSceneInfo
�� 7
(
��7 8
Scene
��8 =
scene
��> C
)
��C D
{
�� 	
var
�� 
sceneContext
�� 
=
��  
ZenUnityEditorUtil
�� 1
.
��1 2(
TryGetSceneContextForScene
��2 L
(
��L M
scene
��M R
)
��R S
;
��S T
var
�� 
decoratorContext
��  
=
��! " 
ZenUnityEditorUtil
��# 5
.
��5 6,
TryGetDecoratorContextForScene
��6 T
(
��T U
scene
��U Z
)
��Z [
;
��[ \
if
�� 
(
�� 
sceneContext
�� 
==
�� 
null
��  $
&&
��% '
decoratorContext
��( 8
==
��9 ;
null
��< @
)
��@ A
{
�� 
return
�� 
null
�� 
;
�� 
}
�� 
var
�� 
info
�� 
=
�� 
new
�� 
LoadedSceneInfo
�� *
{
�� 
Scene
�� 
=
�� 
scene
�� 
}
�� 
;
�� 
if
�� 
(
�� 
sceneContext
�� 
!=
�� 
null
��  $
)
��$ %
{
�� 
Assert
�� 
.
�� 
IsNull
�� 
(
�� 
decoratorContext
�� .
,
��. /
$str
�� R
,
��R S
scene
��T Y
.
��Y Z
name
��Z ^
)
��^ _
;
��_ `
info
�� 
.
�� 
SceneContext
�� !
=
��" #
sceneContext
��$ 0
;
��0 1
}
�� 
else
�� 
{
�� 
Assert
�� 
.
�� 
	IsNotNull
��  
(
��  !
decoratorContext
��! 1
)
��1 2
;
��2 3
info
�� 
.
�� 
DecoratorContext
�� %
=
��& '
decoratorContext
��( 8
;
��8 9
}
�� 
return
�� 
info
�� 
;
�� 
}
�� 	
static
�� 
List
�� 
<
�� 
LoadedSceneInfo
�� #
>
��# $(
GetLoadedZenjectSceneInfos
��% ?
(
��? @
)
��@ A
{
�� 	
var
�� 
result
�� 
=
�� 
new
�� 
List
�� !
<
��! "
LoadedSceneInfo
��" 1
>
��1 2
(
��2 3
)
��3 4
;
��4 5
for
�� 
(
�� 
int
�� 
i
�� 
=
�� 
$num
�� 
;
�� 
i
�� 
<
��  
EditorSceneManager
��  2
.
��2 3

sceneCount
��3 =
;
��= >
i
��? @
++
��@ B
)
��B C
{
�� 
var
�� 
scene
�� 
=
��  
EditorSceneManager
�� .
.
��. /

GetSceneAt
��/ 9
(
��9 :
i
��: ;
)
��; <
;
��< =
var
�� 
info
�� 
=
�� &
TryCreateLoadedSceneInfo
�� 3
(
��3 4
scene
��4 9
)
��9 :
;
��: ;
if
�� 
(
�� 
info
�� 
!=
�� 
null
��  
)
��  !
{
�� 
result
�� 
.
�� 
Add
�� 
(
�� 
info
�� #
)
��# $
;
��$ %
}
�� 
}
�� 
return
�� 
result
�� 
;
�� 
}
�� 	
static
�� 
void
�� 
AddToContractMap
�� $
(
��$ %

Dictionary
�� 
<
�� 
string
�� 
,
�� 
LoadedSceneInfo
�� .
>
��. /
contractMap
��0 ;
,
��; <
LoadedSceneInfo
��= L
info
��M Q
)
��Q R
{
�� 	
if
�� 
(
�� 
info
�� 
.
�� 
SceneContext
�� !
==
��" $
null
��% )
)
��) *
{
�� 
return
�� 
;
�� 
}
�� 
foreach
�� 
(
�� 
var
�� 
contractName
�� %
in
��& (
info
��) -
.
��- .
SceneContext
��. :
.
��: ;
ContractNames
��; H
)
��H I
{
�� 
LoadedSceneInfo
�� 
currentInfo
��  +
;
��+ ,
if
�� 
(
�� 
contractMap
�� 
.
��  
TryGetValue
��  +
(
��+ ,
contractName
��, 8
,
��8 9
out
��: =
currentInfo
��> I
)
��I J
)
��J K
{
�� 
throw
�� 
Assert
��  
.
��  !
CreateException
��! 0
(
��0 1
$str
�� e
,
��e f
contractName
�� $
,
��$ %
currentInfo
��& 1
.
��1 2
Scene
��2 7
.
��7 8
name
��8 <
,
��< =
info
��> B
.
��B C
Scene
��C H
.
��H I
name
��I M
)
��M N
;
��N O
}
�� 
contractMap
�� 
.
�� 
Add
�� 
(
��  
contractName
��  ,
,
��, -
info
��. 2
)
��2 3
;
��3 4
}
�� 
}
�� 	
public
�� 
class
�� 
LoadedSceneInfo
�� $
{
�� 	
public
�� 
SceneContext
�� 
SceneContext
��  ,
;
��, -
public
�� #
SceneDecoratorContext
�� (
DecoratorContext
��) 9
;
��9 :
public
�� 
Scene
�� 
Scene
�� 
;
�� 
}
�� 	
}
�� 
}�� ��
\C:\Users\Климнюк\tring\App\Assets\Plugins\Zenject\Source\Editor\ZenUnityEditorUtil.cs
	namespace 	
Zenject
 
. 
Internal 
{ 
public 

static 
class 
ZenUnityEditorUtil *
{ 
public 
static 
bool )
SaveThenRunPreserveSceneSetup 8
(8 9
Action9 ?
action@ F
)F G
{ 	
if 
( 
EditorSceneManager "
." #2
&SaveCurrentModifiedScenesIfUserWantsTo# I
(I J
)J K
)K L
{ 
var 
originalSceneSetup &
=' (
EditorSceneManager) ;
.; < 
GetSceneManagerSetup< P
(P Q
)Q R
;R S
try 
{ 
action 
( 
) 
; 
return 
true 
;  
} 
catch 
( 
	Exception  
e! "
)" #
{ 
Log 
. 
ErrorException &
(& '
e' (
)( )
;) *
return   
false    
;    !
}!! 
finally"" 
{## 
EditorSceneManager$$ &
.$$& '$
RestoreSceneManagerSetup$$' ?
($$? @
originalSceneSetup$$@ R
)$$R S
;$$S T
}%% 
}&& 
return(( 
false(( 
;(( 
})) 	
public-- 
static-- 
void-- %
ValidateCurrentSceneSetup-- 4
(--4 5
)--5 6
{.. 	
bool// 
encounteredError// !
=//" #
false//$ )
;//) *
Application11 
.11 
LogCallback11 #
logCallback11$ /
=110 1
(112 3
	condition113 <
,11< =

stackTrace11> H
,11H I
type11J N
)11N O
=>11P R
{22 
if33 
(33 
type33 
==33 
LogType33 #
.33# $
Error33$ )
||33* ,
type33- 1
==332 4
LogType335 <
.33< =
Assert33= C
||44 
type44 
==44 
LogType44 &
.44& '
	Exception44' 0
)440 1
{55 
encounteredError66 $
=66% &
true66' +
;66+ ,
}77 
}88 
;88 
Application:: 
.:: 
logMessageReceived:: *
+=::+ -
logCallback::. 9
;::9 :
try<< 
{== 
Assert>> 
.>> 
That>> 
(>> 
!>> 
ProjectContext>> +
.>>+ ,
HasInstance>>, 7
)>>7 8
;>>8 9
ProjectContext?? 
.?? 
ValidateOnNextRun?? 0
=??1 2
true??3 7
;??7 8
foreachAA 
(AA 
varAA 
sceneContextAA )
inAA* ,
GetAllSceneContextsAA- @
(AA@ A
)AAA B
)AAB C
{BB 
sceneContextCC  
.CC  !
ValidateCC! )
(CC) *
)CC* +
;CC+ ,
}DD 
}EE 
catchFF 
(FF 
	ExceptionFF 
eFF 
)FF 
{GG 
LogHH 
.HH 
ErrorExceptionHH "
(HH" #
eHH# $
)HH$ %
;HH% &
encounteredErrorII  
=II! "
trueII# '
;II' (
}JJ 
finallyKK 
{LL 
ApplicationMM 
.MM 
logMessageReceivedMM .
-=MM/ 1
logCallbackMM2 =
;MM= >
}NN 
ifPP 
(PP 
encounteredErrorPP  
)PP  !
{QQ 
throwRR 
newRR 
ZenjectExceptionRR *
(RR* +
$strRR+ f
)RRf g
;RRg h
}SS 
}TT 	
publicXX 
staticXX 
intXX #
ValidateAllActiveScenesXX 1
(XX1 2
)XX2 3
{YY 	
varZZ 
activeScenePathsZZ  
=ZZ! "
EditorBuildSettingsZZ# 6
.ZZ6 7
scenesZZ7 =
.ZZ= >
WhereZZ> C
(ZZC D
xZZD E
=>ZZF H
xZZI J
.ZZJ K
enabledZZK R
)ZZR S
.[[ 
Select[[ 
([[ 
x[[ 
=>[[ 
x[[ 
.[[ 
path[[ #
)[[# $
.[[$ %
ToList[[% +
([[+ ,
)[[, -
;[[- .
foreach]] 
(]] 
var]] 
	scenePath]] "
in]]# %
activeScenePaths]]& 6
)]]6 7
{^^ 
EditorSceneManager__ "
.__" #
	OpenScene__# ,
(__, -
	scenePath__- 6
,__6 7
OpenSceneMode__8 E
.__E F
Single__F L
)__L M
;__M N%
ValidateCurrentSceneSetup`` )
(``) *
)``* +
;``+ ,
}aa 
returncc 
activeScenePathscc #
.cc# $
Countcc$ )
;cc) *
}dd 	
publicgg 
staticgg 
voidgg  
RunCurrentSceneSetupgg /
(gg/ 0
)gg0 1
{hh 	
Assertii 
.ii 
Thatii 
(ii 
!ii 
ProjectContextii '
.ii' (
HasInstanceii( 3
)ii3 4
;ii4 5
foreachkk 
(kk 
varkk 
sceneContextkk %
inkk& (
GetAllSceneContextskk) <
(kk< =
)kk= >
)kk> ?
{ll 
trymm 
{nn 
sceneContextoo  
.oo  !
Runoo! $
(oo$ %
)oo% &
;oo& '
}pp 
catchqq 
(qq 
	Exceptionqq  
eqq! "
)qq" #
{rr 
throwtt 
newtt 
ZenjectExceptiontt .
(tt. /
$struu 6
.uu6 7
Fmtuu7 :
(uu: ;
sceneContextuu; G
.uuG H

gameObjectuuH R
.uuR S
sceneuuS X
.uuX Y
nameuuY ]
)uu] ^
,uu^ _
euu` a
)uua b
;uub c
}vv 
}ww 
}xx 	
publiczz 
staticzz 
SceneContextzz "#
GetSceneContextForScenezz# :
(zz: ;
Scenezz; @
scenezzA F
)zzF G
{{{ 	
var|| 
sceneContext|| 
=|| &
TryGetSceneContextForScene|| 9
(||9 :
scene||: ?
)||? @
;||@ A
Assert~~ 
.~~ 
	IsNotNull~~ 
(~~ 
sceneContext~~ )
,~~) *
$str >
,> ?
scene@ E
.E F
nameF J
)J K
;K L
return
�� 
sceneContext
�� 
;
��  
}
�� 	
public
�� 
static
�� 
SceneContext
�� "(
TryGetSceneContextForScene
��# =
(
��= >
Scene
��> C
scene
��D I
)
��I J
{
�� 	
if
�� 
(
�� 
!
�� 
scene
�� 
.
�� 
isLoaded
�� 
)
��  
{
�� 
return
�� 
null
�� 
;
�� 
}
�� 
var
�� 
sceneContexts
�� 
=
�� 
scene
��  %
.
��% & 
GetRootGameObjects
��& 8
(
��8 9
)
��9 :
.
�� 

SelectMany
�� 
(
�� 
x
�� 
=>
��  
x
��! "
.
��" #%
GetComponentsInChildren
��# :
<
��: ;
SceneContext
��; G
>
��G H
(
��H I
)
��I J
)
��J K
.
��K L
ToList
��L R
(
��R S
)
��S T
;
��T U
if
�� 
(
�� 
sceneContexts
�� 
.
�� 
IsEmpty
�� %
(
��% &
)
��& '
)
��' (
{
�� 
return
�� 
null
�� 
;
�� 
}
�� 
Assert
�� 
.
�� 
That
�� 
(
�� 
sceneContexts
�� %
.
��% &
Count
��& +
==
��, .
$num
��/ 0
,
��0 1
$str
�� Z
,
��Z [
scene
��\ a
.
��a b
name
��b f
)
��f g
;
��g h
return
�� 
sceneContexts
��  
[
��  !
$num
��! "
]
��" #
;
��# $
}
�� 	
public
�� 
static
�� #
SceneDecoratorContext
�� +)
GetDecoratorContextForScene
��, G
(
��G H
Scene
��H M
scene
��N S
)
��S T
{
�� 	
var
�� 
decoratorContext
��  
=
��! ",
TryGetDecoratorContextForScene
��# A
(
��A B
scene
��B G
)
��G H
;
��H I
Assert
�� 
.
�� 
	IsNotNull
�� 
(
�� 
decoratorContext
�� -
,
��- .
$str
�� B
,
��B C
scene
��D I
.
��I J
name
��J N
)
��N O
;
��O P
return
�� 
decoratorContext
�� #
;
��# $
}
�� 	
public
�� 
static
�� #
SceneDecoratorContext
�� +,
TryGetDecoratorContextForScene
��, J
(
��J K
Scene
��K P
scene
��Q V
)
��V W
{
�� 	
if
�� 
(
�� 
!
�� 
scene
�� 
.
�� 
isLoaded
�� 
)
��  
{
�� 
return
�� 
null
�� 
;
�� 
}
�� 
var
�� 
decoratorContexts
�� !
=
��" #
scene
��$ )
.
��) * 
GetRootGameObjects
��* <
(
��< =
)
��= >
.
�� 

SelectMany
�� 
(
�� 
x
�� 
=>
��  
x
��! "
.
��" #%
GetComponentsInChildren
��# :
<
��: ;#
SceneDecoratorContext
��; P
>
��P Q
(
��Q R
)
��R S
)
��S T
.
��T U
ToList
��U [
(
��[ \
)
��\ ]
;
��] ^
if
�� 
(
�� 
decoratorContexts
�� !
.
��! "
IsEmpty
��" )
(
��) *
)
��* +
)
��+ ,
{
�� 
return
�� 
null
�� 
;
�� 
}
�� 
Assert
�� 
.
�� 
That
�� 
(
�� 
decoratorContexts
�� )
.
��) *
Count
��* /
==
��0 2
$num
��3 4
,
��4 5
$str
�� ^
,
��^ _
scene
��` e
.
��e f
name
��f j
)
��j k
;
��k l
return
�� 
decoratorContexts
�� $
[
��$ %
$num
��% &
]
��& '
;
��' (
}
�� 	
static
�� 
IEnumerable
�� 
<
�� 
SceneContext
�� '
>
��' (!
GetAllSceneContexts
��) <
(
��< =
)
��= >
{
�� 	
var
�� !
decoratedSceneNames
�� #
=
��$ %
new
��& )
List
��* .
<
��. /
string
��/ 5
>
��5 6
(
��6 7
)
��7 8
;
��8 9
for
�� 
(
�� 
int
�� 
i
�� 
=
�� 
$num
�� 
;
�� 
i
�� 
<
��  
EditorSceneManager
��  2
.
��2 3

sceneCount
��3 =
;
��= >
i
��? @
++
��@ B
)
��B C
{
�� 
var
�� 
scene
�� 
=
��  
EditorSceneManager
�� .
.
��. /

GetSceneAt
��/ 9
(
��9 :
i
��: ;
)
��; <
;
��< =
var
�� 
sceneContext
��  
=
��! "(
TryGetSceneContextForScene
��# =
(
��= >
scene
��> C
)
��C D
;
��D E
var
�� 
decoratorContext
�� $
=
��% &,
TryGetDecoratorContextForScene
��' E
(
��E F
scene
��F K
)
��K L
;
��L M
if
�� 
(
�� 
sceneContext
��  
!=
��! #
null
��$ (
)
��( )
{
�� 
Assert
�� 
.
�� 
That
�� 
(
��  
decoratorContext
��  0
==
��1 3
null
��4 8
,
��8 9
$str
�� y
,
��y z
scene��{ �
.��� �
name��� �
)��� �
;��� �!
decoratedSceneNames
�� '
.
��' (
	RemoveAll
��( 1
(
��1 2
x
��2 3
=>
��4 6
sceneContext
��7 C
.
��C D
ContractNames
��D Q
.
��Q R
Contains
��R Z
(
��Z [
x
��[ \
)
��\ ]
)
��] ^
;
��^ _
yield
�� 
return
��  
sceneContext
��! -
;
��- .
}
�� 
else
�� 
if
�� 
(
�� 
decoratorContext
�� )
!=
��* ,
null
��- 1
)
��1 2
{
�� 
Assert
�� 
.
�� 
That
�� 
(
��  
!
��  !
string
��! '
.
��' (
IsNullOrEmpty
��( 5
(
��5 6
decoratorContext
��6 F
.
��F G#
DecoratedContractName
��G \
)
��\ ]
,
��] ^
$str
�� a
,
��a b
scene
��c h
.
��h i
name
��i m
)
��m n
;
��n o!
decoratedSceneNames
�� '
.
��' (
Add
��( +
(
��+ ,
decoratorContext
��, <
.
��< =#
DecoratedContractName
��= R
)
��R S
;
��S T
}
�� 
}
�� 
Assert
�� 
.
�� 
That
�� 
(
�� !
decoratedSceneNames
�� +
.
��+ ,
IsEmpty
��, 3
(
��3 4
)
��4 5
,
��5 6
$str
�� r
,
��r s"
decoratedSceneNames��t �
.��� �
Join��� �
(��� �
$str��� �
)��� �
)��� �
;��� �
}
�� 	
public
�� 
static
�� 
string
�� ,
ConvertAssetPathToAbsolutePath
�� ;
(
��; <
string
��< B
	assetPath
��C L
)
��L M
{
�� 	
return
�� 
Path
�� 
.
�� 
Combine
�� 
(
��  
Path
�� 
.
�� 
Combine
�� 
(
�� 
Path
�� !
.
��! "
GetFullPath
��" -
(
��- .
Application
��. 9
.
��9 :
dataPath
��: B
)
��B C
,
��C D
$str
��E I
)
��I J
,
��J K
	assetPath
��L U
)
��U V
;
��V W
}
�� 	
public
�� 
static
�� 
string
�� 0
"ConvertFullAbsolutePathToAssetPath
�� ?
(
��? @
string
��@ F
fullPath
��G O
)
��O P
{
�� 	
fullPath
�� 
=
�� 
Path
�� 
.
�� 
GetFullPath
�� '
(
��' (
fullPath
��( 0
)
��0 1
;
��1 2
var
�� !
assetFolderFullPath
�� #
=
��$ %
Path
��& *
.
��* +
GetFullPath
��+ 6
(
��6 7
Application
��7 B
.
��B C
dataPath
��C K
)
��K L
;
��L M
if
�� 
(
�� 
fullPath
�� 
.
�� 
Length
�� 
==
��  "!
assetFolderFullPath
��# 6
.
��6 7
Length
��7 =
)
��= >
{
�� 
Assert
�� 
.
�� 
IsEqual
�� 
(
�� 
fullPath
�� '
,
��' (!
assetFolderFullPath
��) <
)
��< =
;
��= >
return
�� 
$str
�� 
;
��  
}
�� 
var
�� 
	assetPath
�� 
=
�� 
fullPath
�� $
.
��$ %
Remove
��% +
(
��+ ,
$num
��, -
,
��- .!
assetFolderFullPath
��/ B
.
��B C
Length
��C I
+
��J K
$num
��L M
)
��M N
.
��N O
Replace
��O V
(
��V W
$str
��W [
,
��[ \
$str
��] `
)
��` a
;
��a b
return
�� 
$str
�� 
+
�� 
	assetPath
�� (
;
��( )
}
�� 	
public
�� 
static
�� 
string
�� 7
)GetCurrentDirectoryAssetPathFromSelection
�� F
(
��F G
)
��G H
{
�� 	
return
�� 0
"ConvertFullAbsolutePathToAssetPath
�� 5
(
��5 6:
,GetCurrentDirectoryAbsolutePathFromSelection
�� <
(
��< =
)
��= >
)
��> ?
;
��? @
}
�� 	
public
�� 
static
�� 
string
�� :
,GetCurrentDirectoryAbsolutePathFromSelection
�� I
(
��I J
)
��J K
{
�� 	
var
�� 

folderPath
�� 
=
�� 3
%TryGetSelectedFolderPathInProjectsTab
�� B
(
��B C
)
��C D
;
��D E
if
�� 
(
�� 

folderPath
�� 
!=
�� 
null
�� "
)
��" #
{
�� 
return
�� 

folderPath
�� !
;
��! "
}
�� 
var
�� 
filePath
�� 
=
�� 1
#TryGetSelectedFilePathInProjectsTab
�� >
(
��> ?
)
��? @
;
��@ A
if
�� 
(
�� 
filePath
�� 
!=
�� 
null
��  
)
��  !
{
�� 
return
�� 
Path
�� 
.
�� 
GetDirectoryName
�� ,
(
��, -
filePath
��- 5
)
��5 6
;
��6 7
}
�� 
return
�� 
Application
�� 
.
�� 
dataPath
�� '
;
��' (
}
�� 	
public
�� 
static
�� 
string
�� 1
#TryGetSelectedFilePathInProjectsTab
�� @
(
��@ A
)
��A B
{
�� 	
return
�� /
!GetSelectedFilePathsInProjectsTab
�� 4
(
��4 5
)
��5 6
.
��6 7
OnlyOrDefault
��7 D
(
��D E
)
��E F
;
��F G
}
�� 	
public
�� 
static
�� 
List
�� 
<
�� 
string
�� !
>
��! "/
!GetSelectedFilePathsInProjectsTab
��# D
(
��D E
)
��E F
{
�� 	
return
�� +
GetSelectedPathsInProjectsTab
�� 0
(
��0 1
)
��1 2
.
�� 
Where
�� 
(
�� 
x
�� 
=>
�� 
File
��  
.
��  !
Exists
��! '
(
��' (
x
��( )
)
��) *
)
��* +
.
��+ ,
ToList
��, 2
(
��2 3
)
��3 4
;
��4 5
}
�� 	
public
�� 
static
�� 
List
�� 
<
�� 
string
�� !
>
��! "0
"GetSelectedAssetPathsInProjectsTab
��# E
(
��E F
)
��F G
{
�� 	
var
�� 
paths
�� 
=
�� 
new
�� 
List
��  
<
��  !
string
��! '
>
��' (
(
��( )
)
��) *
;
��* +
UnityEngine
�� 
.
�� 
Object
�� 
[
�� 
]
��  
selectedAssets
��! /
=
��0 1
	Selection
��2 ;
.
��; <
GetFiltered
��< G
(
��G H
typeof
�� 
(
�� 
UnityEngine
�� "
.
��" #
Object
��# )
)
��) *
,
��* +
SelectionMode
��, 9
.
��9 :
Assets
��: @
)
��@ A
;
��A B
foreach
�� 
(
�� 
var
�� 
item
�� 
in
��  
selectedAssets
��! /
)
��/ 0
{
�� 
var
�� 
	assetPath
�� 
=
�� 
AssetDatabase
��  -
.
��- .
GetAssetPath
��. :
(
��: ;
item
��; ?
)
��? @
;
��@ A
if
�� 
(
�� 
!
�� 
string
�� 
.
�� 
IsNullOrEmpty
�� )
(
��) *
	assetPath
��* 3
)
��3 4
)
��4 5
{
�� 
paths
�� 
.
�� 
Add
�� 
(
�� 
	assetPath
�� '
)
��' (
;
��( )
}
�� 
}
�� 
return
�� 
paths
�� 
;
�� 
}
�� 	
public
�� 
static
�� 
List
�� 
<
�� 
string
�� !
>
��! "+
GetSelectedPathsInProjectsTab
��# @
(
��@ A
)
��A B
{
�� 	
var
�� 
paths
�� 
=
�� 
new
�� 
List
��  
<
��  !
string
��! '
>
��' (
(
��( )
)
��) *
;
��* +
UnityEngine
�� 
.
�� 
Object
�� 
[
�� 
]
��  
selectedAssets
��! /
=
��0 1
	Selection
��2 ;
.
��; <
GetFiltered
��< G
(
��G H
typeof
�� 
(
�� 
UnityEngine
�� "
.
��" #
Object
��# )
)
��) *
,
��* +
SelectionMode
��, 9
.
��9 :
Assets
��: @
)
��@ A
;
��A B
foreach
�� 
(
�� 
var
�� 
item
�� 
in
��  
selectedAssets
��! /
)
��/ 0
{
�� 
var
�� 
relativePath
��  
=
��! "
AssetDatabase
��# 0
.
��0 1
GetAssetPath
��1 =
(
��= >
item
��> B
)
��B C
;
��C D
if
�� 
(
�� 
!
�� 
string
�� 
.
�� 
IsNullOrEmpty
�� )
(
��) *
relativePath
��* 6
)
��6 7
)
��7 8
{
�� 
var
�� 
fullPath
��  
=
��! "
Path
��# '
.
��' (
GetFullPath
��( 3
(
��3 4
Path
��4 8
.
��8 9
Combine
��9 @
(
��@ A
Application
�� #
.
��# $
dataPath
��$ ,
,
��, -
Path
��. 2
.
��2 3
Combine
��3 :
(
��: ;
$str
��; ?
,
��? @
relativePath
��A M
)
��M N
)
��N O
)
��O P
;
��P Q
paths
�� 
.
�� 
Add
�� 
(
�� 
fullPath
�� &
)
��& '
;
��' (
}
�� 
}
�� 
return
�� 
paths
�� 
;
�� 
}
�� 	
public
�� 
static
�� 
void
�� '
SaveScriptableObjectAsset
�� 4
(
��4 5
string
�� 
path
�� 
,
�� 
ScriptableObject
�� )
asset
��* /
)
��/ 0
{
�� 	
Assert
�� 
.
�� 
That
�� 
(
�� 
path
�� 
.
�� 
EndsWith
�� %
(
��% &
$str
��& .
)
��. /
)
��/ 0
;
��0 1
string
�� 
assetPathAndName
�� #
=
��$ %
AssetDatabase
��& 3
.
��3 4%
GenerateUniqueAssetPath
��4 K
(
��K L
path
��L P
)
��P Q
;
��Q R
AssetDatabase
�� 
.
�� 
CreateAsset
�� %
(
��% &
asset
��& +
,
��+ ,
assetPathAndName
��- =
)
��= >
;
��> ?
AssetDatabase
�� 
.
�� 

SaveAssets
�� $
(
��$ %
)
��% &
;
��& '
AssetDatabase
�� 
.
�� 
Refresh
�� !
(
��! "
)
��" #
;
��# $
EditorUtility
�� 
.
��  
FocusProjectWindow
�� ,
(
��, -
)
��- .
;
��. /
	Selection
�� 
.
�� 
activeObject
�� "
=
��# $
asset
��% *
;
��* +
}
�� 	
public
�� 
static
�� 
List
�� 
<
�� 
string
�� !
>
��! "1
#GetSelectedFolderPathsInProjectsTab
��# F
(
��F G
)
��G H
{
�� 	
return
�� +
GetSelectedPathsInProjectsTab
�� 0
(
��0 1
)
��1 2
.
�� 
Where
�� 
(
�� 
x
�� 
=>
�� 
	Directory
�� %
.
��% &
Exists
��& ,
(
��, -
x
��- .
)
��. /
)
��/ 0
.
��0 1
ToList
��1 7
(
��7 8
)
��8 9
;
��9 :
}
�� 	
public
�� 
static
�� 
string
�� 3
%TryGetSelectedFolderPathInProjectsTab
�� B
(
��B C
)
��C D
{
�� 	
return
�� 1
#GetSelectedFolderPathsInProjectsTab
�� 6
(
��6 7
)
��7 8
.
��8 9
OnlyOrDefault
��9 F
(
��F G
)
��G H
;
��H I
}
�� 	
}
�� 
}�� �
iC:\Users\Климнюк\tring\App\Assets\Plugins\Zenject\Source\Editor\Editors\GameObjectContextEditor.cs
	namespace 	
Zenject
 
{ 
[ 
CustomEditor 
( 
typeof 
( 
GameObjectContext *
)* +
)+ ,
], -
[ 
NoReflectionBaking 
] 
public		 

class		 #
GameObjectContextEditor		 (
:		) *!
RunnableContextEditor		+ @
{

 
SerializedProperty 
_kernel "
;" #
public 
override 
void 
OnEnable %
(% &
)& '
{ 	
base 
. 
OnEnable 
( 
) 
; 
_kernel 
= 
serializedObject &
.& '
FindProperty' 3
(3 4
$str4 =
)= >
;> ?
} 	
	protected 
override 
void 
OnGui  %
(% &
)& '
{ 	
base 
. 
OnGui 
( 
) 
; 
EditorGUILayout 
. 
PropertyField )
() *
_kernel* 1
)1 2
;2 3
} 	
} 
} �	
gC:\Users\Климнюк\tring\App\Assets\Plugins\Zenject\Source\Editor\Editors\RunnableContextEditor.cs
	namespace 	
Zenject
 
{ 
[ 
NoReflectionBaking 
] 
public 

class !
RunnableContextEditor &
:' (
ContextEditor) 6
{		 
SerializedProperty

 
_autoRun

 #
;

# $
public 
override 
void 
OnEnable %
(% &
)& '
{ 	
base 
. 
OnEnable 
( 
) 
; 
_autoRun 
= 
serializedObject '
.' (
FindProperty( 4
(4 5
$str5 ?
)? @
;@ A
} 	
	protected 
override 
void 
OnGui  %
(% &
)& '
{ 	
base 
. 
OnGui 
( 
) 
; 
EditorGUILayout 
. 
PropertyField )
() *
_autoRun* 2
)2 3
;3 4
} 	
} 
} ��
VC:\Users\Климнюк\tring\App\Assets\Plugins\Zenject\Source\Editor\ZenMenuItems.cs
	namespace		 	
Zenject		
 
.		 
Internal		 
{

 
public 

static 
class 
ZenMenuItems $
{ 
[ 	
MenuItem	 
( 
$str <
)< =
]= >
public 
static 
void  
ValidateCurrentScene /
(/ 0
)0 1
{ 	(
ValidateCurrentSceneInternal (
(( )
)) *
;* +
} 	
[ 	
MenuItem	 
( 
$str 6
)6 7
]7 8
public 
static 
void '
ValidateCurrentSceneThenRun 6
(6 7
)7 8
{ 	
if 
( (
ValidateCurrentSceneInternal ,
(, -
)- .
). /
{ 
EditorApplication !
.! "
	isPlaying" +
=, -
true. 2
;2 3
} 
} 	
[ 	
MenuItem	 
( 
$str (
)( )
]) *
public 
static 
void 
OpenDocumentation ,
(, -
)- .
{   	
Application!! 
.!! 
OpenURL!! 
(!!  
$str!!  G
)!!G H
;!!H I
}"" 	
[$$ 	
MenuItem$$	 
($$ 
$str$$ 4
,$$4 5
false$$6 ;
,$$; <
$num$$= >
)$$> ?
]$$? @
public%% 
static%% 
void%% 
CreateSceneContext%% -
(%%- .
MenuCommand%%. 9
menuCommand%%: E
)%%E F
{&& 	
var'' 
root'' 
='' 
new'' 

GameObject'' %
(''% &
$str''& 4
)''4 5
.''5 6
AddComponent''6 B
<''B C
SceneContext''C O
>''O P
(''P Q
)''Q R
;''R S
	Selection(( 
.(( 
activeGameObject(( &
=((' (
root(() -
.((- .

gameObject((. 8
;((8 9
EditorSceneManager** 
.** 
MarkSceneDirty** -
(**- .
EditorSceneManager**. @
.**@ A
GetActiveScene**A O
(**O P
)**P Q
)**Q R
;**R S
}++ 	
[-- 	
MenuItem--	 
(-- 
$str-- 8
,--8 9
false--: ?
,--? @
$num--A B
)--B C
]--C D
public.. 
static.. 
void.. "
CreateDecoratorContext.. 1
(..1 2
MenuCommand..2 =
menuCommand..> I
)..I J
{// 	
var00 
root00 
=00 
new00 

GameObject00 %
(00% &
$str00& 8
)008 9
.009 :
AddComponent00: F
<00F G!
SceneDecoratorContext00G \
>00\ ]
(00] ^
)00^ _
;00_ `
	Selection11 
.11 
activeGameObject11 &
=11' (
root11) -
.11- .

gameObject11. 8
;118 9
EditorSceneManager33 
.33 
MarkSceneDirty33 -
(33- .
EditorSceneManager33. @
.33@ A
GetActiveScene33A O
(33O P
)33P Q
)33Q R
;33R S
}44 	
[66 	
MenuItem66	 
(66 
$str66 :
,66: ;
false66< A
,66A B
$num66C D
)66D E
]66E F
public77 
static77 
void77 #
CreateGameObjectContext77 2
(772 3
MenuCommand773 >
menuCommand77? J
)77J K
{88 	
var99 
root99 
=99 
new99 

GameObject99 %
(99% &
$str99& 9
)999 :
.99: ;
AddComponent99; G
<99G H
GameObjectContext99H Y
>99Y Z
(99Z [
)99[ \
;99\ ]
	Selection:: 
.:: 
activeGameObject:: &
=::' (
root::) -
.::- .

gameObject::. 8
;::8 9
EditorSceneManager<< 
.<< 
MarkSceneDirty<< -
(<<- .
EditorSceneManager<<. @
.<<@ A
GetActiveScene<<A O
(<<O P
)<<P Q
)<<Q R
;<<R S
}== 	
[?? 	
MenuItem??	 
(?? 
$str?? 7
)??7 8
]??8 9
public@@ 
static@@ 
void@@ 1
%CreateProjectContextInDefaultLocation@@ @
(@@@ A
)@@A B
{AA 	
varBB 
fullDirPathBB 
=BB 
PathBB "
.BB" #
CombineBB# *
(BB* +
ApplicationBB+ 6
.BB6 7
dataPathBB7 ?
,BB? @
$strBBA L
)BBL M
;BBM N
ifDD 
(DD 
!DD 
	DirectoryDD 
.DD 
ExistsDD !
(DD! "
fullDirPathDD" -
)DD- .
)DD. /
{EE 
	DirectoryFF 
.FF 
CreateDirectoryFF )
(FF) *
fullDirPathFF* 5
)FF5 6
;FF6 7
}GG (
CreateProjectContextInternalII (
(II( )
$strII) ;
)II; <
;II< =
}JJ 	
[LL 	
MenuItemLL	 
(LL 
$strLL G
,LLG H
falseLLI N
,LLN O
$numLLP R
)LLR S
]LLS T
publicMM 
staticMM 
voidMM ,
 CreateDefaultSceneContractConfigMM ;
(MM; <
)MM< =
{NN 	
varOO 

folderPathOO 
=OO 
ZenUnityEditorUtilOO /
.OO/ 05
)GetCurrentDirectoryAssetPathFromSelectionOO0 Y
(OOY Z
)OOZ [
;OO[ \
ifQQ 
(QQ 
!QQ 

folderPathQQ 
.QQ 
EndsWithQQ $
(QQ$ %
$strQQ% 1
)QQ1 2
)QQ2 3
{RR 
EditorUtilitySS 
.SS 
DisplayDialogSS +
(SS+ ,
$strSS, 3
,SS3 4
$str	TT �
,
TT� �
$str
TT� �
)
TT� �
;
TT� �
returnUU 
;UU 
}VV 
varXX 
configXX 
=XX 
ScriptableObjectXX )
.XX) *
CreateInstanceXX* 8
<XX8 9&
DefaultSceneContractConfigXX9 S
>XXS T
(XXT U
)XXU V
;XXV W
ZenUnityEditorUtilZZ 
.ZZ %
SaveScriptableObjectAssetZZ 8
(ZZ8 9
Path[[ 
.[[ 
Combine[[ 
([[ 

folderPath[[ '
,[[' (&
DefaultSceneContractConfig[[) C
.[[C D
ResourcePath[[D P
+[[Q R
$str[[S [
)[[[ \
,[[\ ]
config[[^ d
)[[d e
;[[e f
}\\ 	
[^^ 	
MenuItem^^	 
(^^ 
$str^^ E
,^^E F
false^^G L
,^^L M
$num^^N O
)^^O P
]^^P Q
public__ 
static__ 
void__ +
CreateScriptableObjectInstaller__ :
(__: ;
)__; <
{`` 	"
AddCSharpClassTemplateaa "
(aa" #
$straa# @
,aa@ A
$straaB U
,aaU V
$strbb &
+cc 
$strcc $
+dd 
$strdd 
+ee 
$stree h
+ff 
$strff U
+gg 
$strgg 
+hh 
$strhh @
+ii 
$strii 
+jj 
$strjj 
+kk 
$strkk 
)kk 
;kk 
}ll 	
[nn 	
MenuItemnn	 
(nn 
$strnn 8
,nn8 9
falsenn: ?
,nn? @
$numnnA B
)nnB C
]nnC D
publicoo 
staticoo 
voidoo 
CreateMonoInstalleroo .
(oo. /
)oo/ 0
{pp 	"
AddCSharpClassTemplateqq "
(qq" #
$strqq# 3
,qq3 4
$strqq5 H
,qqH I
$strrr &
+ss 
$strss $
+tt 
$strtt 
+uu 
$struu =
+vv 
$strvv 
+ww 
$strww @
+xx 
$strxx 
+yy 
$stryy 
+zz 
$strzz 
)zz 
;zz 
}{{ 	
[}} 	
MenuItem}}	 
(}} 
$str}} 3
,}}3 4
false}}5 :
,}}: ;
$num}}< =
)}}= >
]}}> ?
public~~ 
static~~ 
void~~ 
CreateInstaller~~ *
(~~* +
)~~+ ,
{ 	$
AddCSharpClassTemplate
�� "
(
��" #
$str
��# .
,
��. /
$str
��0 C
,
��C D
$str
�� &
+
�� 
$str
�� $
+
�� 
$str
�� 
+
�� 
$str
�� E
+
�� 
$str
�� 
+
�� 
$str
�� @
+
�� 
$str
�� 
+
�� 
$str
�� 
+
�� 
$str
�� 
)
�� 
;
�� 
}
�� 	
[
�� 	
MenuItem
��	 
(
�� 
$str
�� 7
,
��7 8
false
��9 >
,
��> ?
$num
��@ B
)
��B C
]
��C D
public
�� 
static
�� 
void
��  
CreateEditorWindow
�� -
(
��- .
)
��. /
{
�� 	$
AddCSharpClassTemplate
�� "
(
��" #
$str
��# 2
,
��2 3
$str
��4 J
,
��J K
$str
�� &
+
�� 
$str
�� (
+
�� 
$str
�� $
+
�� 
$str
�� 
+
�� 
$str
�� C
+
�� 
$str
�� 
+
�� 
$str
�� ;
+
�� 
$str
�� F
+
�� 
$str
�� 
+
�� 
$str
�� P
+
�� 
$str
�� S
+
�� 
$str
�� ,
+
�� 
$str
�� 
+
�� 
$str
�� 
+
�� 
$str
�� @
+
�� 
$str
�� 
+
�� 
$str
�� %
+
�� 
$str
�� 
+
�� 
$str
�� 
)
�� 
;
�� 
}
�� 	
[
�� 	
MenuItem
��	 
(
�� 
$str
�� 9
,
��9 :
false
��; @
,
��@ A
$num
��B D
)
��D E
]
��E F
public
�� 
static
�� 
void
�� "
CreateProjectContext
�� /
(
��/ 0
)
��0 1
{
�� 	
var
�� 
absoluteDir
�� 
=
��  
ZenUnityEditorUtil
�� 0
.
��0 13
%TryGetSelectedFolderPathInProjectsTab
��1 V
(
��V W
)
��W X
;
��X Y
if
�� 
(
�� 
absoluteDir
�� 
==
�� 
null
�� #
)
��# $
{
�� 
EditorUtility
�� 
.
�� 
DisplayDialog
�� +
(
��+ ,
$str
��, 3
,
��3 4
$str�� �
.
�� 
Fmt
�� 
(
�� 
ProjectContext
�� '
.
��' ((
ProjectContextResourcePath
��( B
)
��B C
,
��C D
$str
��E I
)
��I J
;
��J K
return
�� 
;
�� 
}
�� 
var
�� 
parentFolderName
��  
=
��! "
Path
��# '
.
��' (
GetFileName
��( 3
(
��3 4
absoluteDir
��4 ?
)
��? @
;
��@ A
if
�� 
(
�� 
parentFolderName
��  
!=
��! #
$str
��$ /
)
��/ 0
{
�� 
EditorUtility
�� 
.
�� 
DisplayDialog
�� +
(
��+ ,
$str
��, 3
,
��3 4
$str�� �
.
�� 
Fmt
�� 
(
�� 
ProjectContext
�� '
.
��' ((
ProjectContextResourcePath
��( B
)
��B C
,
��C D
$str
��E I
)
��I J
;
��J K
return
�� 
;
�� 
}
�� *
CreateProjectContextInternal
�� (
(
��( )
absoluteDir
��) 4
)
��4 5
;
��5 6
}
�� 	
static
�� 
void
�� *
CreateProjectContextInternal
�� 0
(
��0 1
string
��1 7
absoluteDir
��8 C
)
��C D
{
�� 	
var
�� 
	assetPath
�� 
=
��  
ZenUnityEditorUtil
�� .
.
��. /0
"ConvertFullAbsolutePathToAssetPath
��/ Q
(
��Q R
absoluteDir
��R ]
)
��] ^
;
��^ _
var
�� 

prefabPath
�� 
=
�� 
(
�� 
Path
�� "
.
��" #
Combine
��# *
(
��* +
	assetPath
��+ 4
,
��4 5
ProjectContext
��6 D
.
��D E(
ProjectContextResourcePath
��E _
)
��_ `
+
��a b
$str
��c l
)
��l m
.
��m n
Replace
��n u
(
��u v
$str
��v z
,
��z {
$str
��| 
)�� �
;��� �
var
�� 

gameObject
�� 
=
�� 
new
��  

GameObject
��! +
(
��+ ,
)
��, -
;
��- .
try
�� 
{
�� 

gameObject
�� 
.
�� 
AddComponent
�� '
<
��' (
ProjectContext
��( 6
>
��6 7
(
��7 8
)
��8 9
;
��9 :
var
�� 
	prefabObj
�� 
=
�� 
PrefabUtility
��  -
.
��- .
SaveAsPrefabAsset
��. ?
(
��? @

gameObject
��@ J
,
��J K

prefabPath
��L V
)
��V W
;
��W X
	Selection
�� 
.
�� 
activeObject
�� &
=
��' (
	prefabObj
��) 2
;
��2 3
}
�� 
finally
�� 
{
�� 

GameObject
�� 
.
�� 
DestroyImmediate
�� +
(
��+ ,

gameObject
��, 6
)
��6 7
;
��7 8
}
�� 
Debug
�� 
.
�� 
Log
�� 
(
�� 
$str
�� ;
.
��; <
Fmt
��< ?
(
��? @

prefabPath
��@ J
)
��J K
)
��K L
;
��L M
}
�� 	
public
�� 
static
�� 
string
�� $
AddCSharpClassTemplate
�� 3
(
��3 4
string
�� 
friendlyName
�� 
,
��  
string
��! '
defaultFileName
��( 7
,
��7 8
string
��9 ?
templateStr
��@ K
)
��K L
{
�� 	
return
�� $
AddCSharpClassTemplate
�� )
(
��) *
friendlyName
�� 
,
�� 
defaultFileName
�� -
,
��- .
templateStr
��/ :
,
��: ; 
ZenUnityEditorUtil
��< N
.
��N O7
)GetCurrentDirectoryAssetPathFromSelection
��O x
(
��x y
)
��y z
)
��z {
;
��{ |
}
�� 	
public
�� 
static
�� 
string
�� $
AddCSharpClassTemplate
�� 3
(
��3 4
string
�� 
friendlyName
�� 
,
��  
string
��! '
defaultFileName
��( 7
,
��7 8
string
�� 
templateStr
�� 
,
�� 
string
��  &

folderPath
��' 1
)
��1 2
{
�� 	
var
�� 
absolutePath
�� 
=
�� 
EditorUtility
�� ,
.
��, -
SaveFilePanel
��- :
(
��: ;
$str
�� "
+
��# $
friendlyName
��% 1
,
��1 2

folderPath
�� 
,
�� 
defaultFileName
�� 
+
��  !
$str
��" '
,
��' (
$str
�� 
)
�� 
;
�� 
if
�� 
(
�� 
absolutePath
�� 
==
�� 
$str
��  "
)
��" #
{
�� 
return
�� 
null
�� 
;
�� 
}
�� 
if
�� 
(
�� 
!
�� 
absolutePath
�� 
.
�� 
ToLower
�� %
(
��% &
)
��& '
.
��' (
EndsWith
��( 0
(
��0 1
$str
��1 6
)
��6 7
)
��7 8
{
�� 
absolutePath
�� 
+=
�� 
$str
��  %
;
��% &
}
�� 
var
�� 
	className
�� 
=
�� 
Path
��  
.
��  !)
GetFileNameWithoutExtension
��! <
(
��< =
absolutePath
��= I
)
��I J
;
��J K
File
�� 
.
�� 
WriteAllText
�� 
(
�� 
absolutePath
�� *
,
��* +
templateStr
��, 7
.
��7 8
Replace
��8 ?
(
��? @
$str
��@ L
,
��L M
	className
��N W
)
��W X
)
��X Y
;
��Y Z
AssetDatabase
�� 
.
�� 
Refresh
�� !
(
��! "
)
��" #
;
��# $
var
�� 
	assetPath
�� 
=
��  
ZenUnityEditorUtil
�� .
.
��. /0
"ConvertFullAbsolutePathToAssetPath
��/ Q
(
��Q R
absolutePath
��R ^
)
��^ _
;
��_ `
EditorUtility
�� 
.
��  
FocusProjectWindow
�� ,
(
��, -
)
��- .
;
��. /
	Selection
�� 
.
�� 
activeObject
�� "
=
��# $
AssetDatabase
��% 2
.
��2 3
LoadAssetAtPath
��3 B
<
��B C
UnityEngine
��C N
.
��N O
Object
��O U
>
��U V
(
��V W
	assetPath
��W `
)
��` a
;
��a b
return
�� 
	assetPath
�� 
;
�� 
}
�� 	
[
�� 	
MenuItem
��	 
(
�� 
$str
�� ;
)
��; <
]
��< =
public
�� 
static
�� 
void
�� %
ValidateAllActiveScenes
�� 2
(
��2 3
)
��3 4
{
�� 	 
ZenUnityEditorUtil
�� 
.
�� +
SaveThenRunPreserveSceneSetup
�� <
(
��< =
(
��= >
)
��> ?
=>
��@ B
{
�� 
var
�� 
numValidated
�� $
=
��% & 
ZenUnityEditorUtil
��' 9
.
��9 :%
ValidateAllActiveScenes
��: Q
(
��Q R
)
��R S
;
��S T
Log
�� 
.
�� 
Info
�� 
(
�� 
$str
�� M
,
��M N
numValidated
��O [
)
��[ \
;
��\ ]
}
�� 
)
�� 
;
�� 
}
�� 	
static
�� 
bool
�� *
ValidateCurrentSceneInternal
�� 0
(
��0 1
)
��1 2
{
�� 	
return
��  
ZenUnityEditorUtil
�� %
.
��% &+
SaveThenRunPreserveSceneSetup
��& C
(
��C D
(
��D E
)
��E F
=>
��G I
{
�� (
SceneParentAutomaticLoader
�� .
.
��. /?
1ValidateMultiSceneSetupAndLoadDefaultSceneParents
��/ `
(
��` a
)
��a b
;
��b c 
ZenUnityEditorUtil
�� &
.
��& ''
ValidateCurrentSceneSetup
��' @
(
��@ A
)
��A B
;
��B C
Log
�� 
.
�� 
Info
�� 
(
�� 
$str
�� @
)
��@ A
;
��A B
}
�� 
)
�� 
;
�� 
}
�� 	
[
�� 	
MenuItem
��	 
(
�� 
$str
�� 3
,
��3 4
false
��5 :
,
��: ;
$num
��< >
)
��> ?
]
��? @
public
�� 
static
�� 
void
�� 
CreateUnitTest
�� )
(
��) *
)
��* +
{
�� 	$
AddCSharpClassTemplate
�� "
(
��" #
$str
��# .
,
��. /
$str
��0 B
,
��B C
$str
�� "
+
�� 
$str
�� ,
+
�� 
$str
�� 
+
�� 
$str
�� #
+
�� 
$str
�� F
+
�� 
$str
�� 
+
�� 
$str
��  
+
�� 
$str
�� 0
+
�� 
$str
�� 
+
�� 
$str
�� %
+
�� 
$str
�� 
+
�� 
$str
�� 
)
�� 
;
�� 
}
�� 	
[
�� 	
MenuItem
��	 
(
�� 
$str
�� :
,
��: ;
false
��< A
,
��A B
$num
��C E
)
��E F
]
��F G
public
�� 
static
�� 
void
�� #
CreateIntegrationTest
�� 0
(
��0 1
)
��1 2
{
�� 	$
AddCSharpClassTemplate
�� "
(
��" #
$str
��# 5
,
��5 6
$str
��7 P
,
��P Q
$str
�� "
+
�� 
$str
�� /
+
�� 
$str
�� 2
+
�� 
$str
�� 
+
�� 
$str
�� M
+
�� 
$str
�� 
+
�� 
$str
�� %
+
�� 
$str
�� 7
+
�� 
$str
�� 
+
�� 
$str
�� w
+
�� 
$str
�� 
+
�� 
$str
�� +
+
�� 
$str
�� 
+
�� 
$str
�� <
+
�� 
$str
�� 
+
�� 
$str
�� ,
+
�� 
$str
�� 
+
�� 
$str
�� G
+
�� 
$str
�� K
+
�� 
$str
�� *
+
�� 
$str
�� 
+
�� 
$str
�� 
)
�� 
;
�� 
}
�� 	
[
�� 	
MenuItem
��	 
(
�� 
$str
�� 4
,
��4 5
false
��6 ;
,
��; <
$num
��= ?
)
��? @
]
��@ A
public
�� 
static
�� 
void
�� 
CreateSceneTest
�� *
(
��* +
)
��+ ,
{
�� 	$
AddCSharpClassTemplate
�� "
(
��" #
$str
��# 7
,
��7 8
$str
��9 L
,
��L M
$str
�� "
+
�� 
$str
�� /
+
�� 
$str
�� (
+
�� 
$str
�� 2
+
�� 
$str
�� 
+
�� 
$str
�� @
+
�� 
$str
�� 
+
�� 
$str
�� %
+
�� 
$str
�� 8
+
�� 
$str
�� 
+
�� 
$str
�� N
+
�� 
$str
�� 
+
�� 
$str
�� Y
+
�� 
$str
�� 
+
�� 
$str
�� F
+
�� 
$str
�� 
+
�� 
$str
�� }
+
�� 
$str
�� 
+
�� 
$str
�� 
)
�� 
;
�� 
}
�� 	
}
�� 
}�� �
fC:\Users\Климнюк\tring\App\Assets\Plugins\Zenject\Source\Editor\Editors\ProjectContextEditor.cs
	namespace 	
Zenject
 
{ 
[ 
CustomEditor 
( 
typeof 
( 
ProjectContext '
)' (
)( )
]) *
[ 
NoReflectionBaking 
] 
public		 

class		  
ProjectContextEditor		 %
:		& '
ContextEditor		( 5
{

 
SerializedProperty 
_settingsProperty ,
;, -
SerializedProperty 7
+_editorReflectionBakingCoverageModeProperty F
;F G
SerializedProperty 7
+_buildsReflectionBakingCoverageModeProperty F
;F G
public 
override 
void 
OnEnable %
(% &
)& '
{ 	
base 
. 
OnEnable 
( 
) 
; 
_settingsProperty 
= 
serializedObject  0
.0 1
FindProperty1 =
(= >
$str> I
)I J
;J K7
+_editorReflectionBakingCoverageModeProperty 7
=8 9
serializedObject: J
.J K
FindPropertyK W
(W X
$strX }
)} ~
;~ 7
+_buildsReflectionBakingCoverageModeProperty 7
=8 9
serializedObject: J
.J K
FindPropertyK W
(W X
$strX }
)} ~
;~ 
} 	
	protected 
override 
void 
OnGui  %
(% &
)& '
{ 	
base 
. 
OnGui 
( 
) 
; 
EditorGUILayout 
. 
PropertyField )
() *
_settingsProperty* ;
,; <
true= A
)A B
;B C
EditorGUILayout 
. 
PropertyField )
() *7
+_editorReflectionBakingCoverageModeProperty* U
,U V
trueW [
)[ \
;\ ]
EditorGUILayout 
. 
PropertyField )
() *7
+_buildsReflectionBakingCoverageModeProperty* U
,U V
trueW [
)[ \
;\ ]
} 	
}   
}!! �
wC:\Users\Климнюк\tring\App\Assets\Plugins\Zenject\Source\Editor\SceneParentLoading\DefaultSceneContractConfig.cs
	namespace 	
Zenject
 
. 
Internal 
{ 
public 

class &
DefaultSceneContractConfig +
:, -
ScriptableObject. >
{		 
public

 
const

 
string

 
ResourcePath

 (
=

) *
$str

+ N
;

N O
public 
List 
< 
ContractInfo  
>  !
DefaultContracts" 2
;2 3
[ 	
Serializable	 
] 
public 
class 
ContractInfo !
{ 	
public 
string 
ContractName &
;& '
public 

SceneAsset 
Scene #
;# $
} 	
} 
} �
mC:\Users\Климнюк\tring\App\Assets\Plugins\Zenject\Source\Editor\Editors\SceneDecoratorContextEditor.cs
	namespace 	
Zenject
 
{ 
[ 
CustomEditor 
( 
typeof 
( !
SceneDecoratorContext .
). /
)/ 0
]0 1
[ 
NoReflectionBaking 
] 
public 

class '
SceneDecoratorContextEditor ,
:- .
ContextEditor/ <
{ 
SerializedProperty *
_decoratedContractNameProperty 9
;9 :
	protected 
override 
string !
[! "
]" #
PropertyNames$ 1
{ 	
get 
{ 
return 
base 
. 
PropertyNames )
.) *
Concat* 0
(0 1
new1 4
string5 ;
[; <
]< =
{ 
$str )
,) *
$str /
,/ 0
$str 9
} 
) 
. 
ToArray 
( 
) 
; 
}   
}!! 	
	protected## 
override## 
string## !
[##! "
]##" # 
PropertyDisplayNames##$ 8
{$$ 	
get%% 
{&& 
return'' 
base'' 
.''  
PropertyDisplayNames'' 0
.''0 1
Concat''1 7
(''7 8
new''8 ;
string''< B
[''B C
]''C D
{(( 
$str)) )
,))) *
$str** 0
,**0 1
$str++ ;
},, 
),, 
.-- 
ToArray-- 
(-- 
)-- 
;-- 
}.. 
}// 	
	protected11 
override11 
string11 !
[11! "
]11" # 
PropertyDescriptions11$ 8
{22 	
get33 
{44 
return55 
base55 
.55  
PropertyDescriptions55 0
.550 1
Concat551 7
(557 8
new558 ;
string55< B
[55B C
]55C D
{66 
$str	77 �
,
77� �
$str	88 �
,
88� �
$str	99 �
}:: 
):: 
.;; 
ToArray;; 
(;; 
);; 
;;; 
}<< 
}== 	
public?? 
override?? 
void?? 
OnEnable?? %
(??% &
)??& '
{@@ 	
baseAA 
.AA 
OnEnableAA 
(AA 
)AA 
;AA *
_decoratedContractNamePropertyCC *
=CC+ ,
serializedObjectCC- =
.CC= >
FindPropertyCC> J
(CCJ K
$strCCK c
)CCc d
;CCd e
}DD 	
	protectedFF 
overrideFF 
voidFF 
OnGuiFF  %
(FF% &
)FF& '
{GG 	
baseHH 
.HH 
OnGuiHH 
(HH 
)HH 
;HH 
EditorGUILayoutJJ 
.JJ 
PropertyFieldJJ )
(JJ) **
_decoratedContractNamePropertyJJ* H
)JJH I
;JJI J
}KK 	
}LL 
}MM �@
_C:\Users\Климнюк\tring\App\Assets\Plugins\Zenject\Source\Editor\ObjectGraphVisualizer.cs
	namespace 	
Zenject
 
{ 
public 

static 
class !
ObjectGraphVisualizer -
{ 
public 
static 
void #
OutputObjectGraphToFile 2
(2 3
DiContainer 
	container !
,! "
string# )

outputPath* 4
,4 5
IEnumerable 
< 
Type 
> 
externalIgnoreTypes 1
,1 2
IEnumerable3 >
<> ?
Type? C
>C D
contractTypesE R
)R S
{ 	
var 
graph 
=  
CalculateObjectGraph ,
(, -
	container- 6
,6 7
contractTypes8 E
)E F
;F G
var 
ignoreTypes 
= 
new !
List" &
<& '
Type' +
>+ ,
{ 
typeof 
( 
DiContainer "
)" #
,# $
typeof 
(  
InitializableManager +
)+ ,
} 
; 
ignoreTypes 
. 
AddRange  
(  !
externalIgnoreTypes! 4
)4 5
;5 6
var 
	resultStr 
= 
$str *
;* +
	resultStr   
+=   
$str   (
;  ( )
foreach"" 
("" 
var"" 
entry"" 
in"" !
graph""" '
)""' (
{## 
if$$ 
($$ 
ShouldIgnoreType$$ $
($$$ %
entry$$% *
.$$* +
Key$$+ .
,$$. /
ignoreTypes$$0 ;
)$$; <
)$$< =
{%% 
continue&& 
;&& 
}'' 
foreach)) 
()) 
var)) 
dependencyType)) +
in)), .
entry))/ 4
.))4 5
Value))5 :
))): ;
{** 
if++ 
(++ 
ShouldIgnoreType++ (
(++( )
dependencyType++) 7
,++7 8
ignoreTypes++9 D
)++D E
)++E F
{,, 
continue--  
;--  !
}.. 
	resultStr00 
+=00   
GetFormattedTypeName00! 5
(005 6
entry006 ;
.00; <
Key00< ?
)00? @
+00A B
$str00C I
+00J K 
GetFormattedTypeName00L `
(00` a
dependencyType00a o
)00o p
+00q r
$str00s y
;00y z
}11 
}22 
	resultStr44 
+=44 
$str44 
;44 
File66 
.66 
WriteAllText66 
(66 

outputPath66 (
,66( )
	resultStr66* 3
)663 4
;664 5
}77 	
static99 
bool99 
ShouldIgnoreType99 $
(99$ %
Type99% )
type99* .
,99. /
List990 4
<994 5
Type995 9
>999 :
ignoreTypes99; F
)99F G
{:: 	
return;; 
ignoreTypes;; 
.;; 
Contains;; '
(;;' (
type;;( ,
);;, -
;;;- .
}<< 	
static>> 

Dictionary>> 
<>> 
Type>> 
,>> 
List>>  $
<>>$ %
Type>>% )
>>>) *
>>>* + 
CalculateObjectGraph>>, @
(>>@ A
DiContainer?? 
	container?? !
,??! "
IEnumerable??# .
<??. /
Type??/ 3
>??3 4
	contracts??5 >
)??> ?
{@@ 	
varAA 
mapAA 
=AA 
newAA 

DictionaryAA $
<AA$ %
TypeAA% )
,AA) *
ListAA+ /
<AA/ 0
TypeAA0 4
>AA4 5
>AA5 6
(AA6 7
)AA7 8
;AA8 9
foreachCC 
(CC 
varCC 
contractTypeCC %
inCC& (
	contractsCC) 2
)CC2 3
{DD 
varEE 
dependsEE 
=EE 
GetDependenciesEE -
(EE- .
	containerEE. 7
,EE7 8
contractTypeEE9 E
)EEE F
;EEF G
ifGG 
(GG 
dependsGG 
.GG 
AnyGG 
(GG  
)GG  !
)GG! "
{HH 
mapII 
.II 
AddII 
(II 
contractTypeII (
,II( )
dependsII* 1
)II1 2
;II2 3
}JJ 
}KK 
returnMM 
mapMM 
;MM 
}NN 	
staticPP 
ListPP 
<PP 
TypePP 
>PP 
GetDependenciesPP )
(PP) *
DiContainerQQ 
	containerQQ !
,QQ! "
TypeQQ# '
typeQQ( ,
)QQ, -
{RR 	
varSS 
dependenciesSS 
=SS 
newSS "
ListSS# '
<SS' (
TypeSS( ,
>SS, -
(SS- .
)SS. /
;SS/ 0
foreachUU 
(UU 
varUU 
contractTypeUU %
inUU& (
	containerUU) 2
.UU2 3"
GetDependencyContractsUU3 I
(UUI J
typeUUJ N
)UUN O
)UUO P
{VV 
ListWW 
<WW 
TypeWW 
>WW 
dependTypesWW &
;WW& '
ifYY 
(YY 
contractTypeYY  
.YY  !
FullNameYY! )
.YY) *

StartsWithYY* 4
(YY4 5
$strYY5 V
)YYV W
)YYW X
{ZZ 
var[[ 
subTypes[[  
=[[! "
contractType[[# /
.[[/ 0
GenericArguments[[0 @
([[@ A
)[[A B
;[[B C
Assert\\ 
.\\ 
IsEqual\\ "
(\\" #
subTypes\\# +
.\\+ ,
Length\\, 2
,\\2 3
$num\\4 5
)\\5 6
;\\6 7
var^^ 
subType^^ 
=^^  !
subTypes^^" *
[^^* +
$num^^+ ,
]^^, -
;^^- .
dependTypes__ 
=__  !
	container__" +
.__+ ,
ResolveTypeAll__, :
(__: ;
subType__; B
)__B C
;__C D
}`` 
elseaa 
{bb 
dependTypescc 
=cc  !
	containercc" +
.cc+ ,
ResolveTypeAllcc, :
(cc: ;
contractTypecc; G
)ccG H
;ccH I
Assertdd 
.dd 
Thatdd 
(dd  
dependTypesdd  +
.dd+ ,
Countdd, 1
<=dd2 4
$numdd5 6
)dd6 7
;dd7 8
}ee 
foreachgg 
(gg 
vargg 

dependTypegg '
ingg( *
dependTypesgg+ 6
)gg6 7
{hh 
dependenciesii  
.ii  !
Addii! $
(ii$ %

dependTypeii% /
)ii/ 0
;ii0 1
}jj 
}kk 
returnmm 
dependenciesmm 
;mm  
}nn 	
staticpp 
stringpp  
GetFormattedTypeNamepp *
(pp* +
Typepp+ /
typepp0 4
)pp4 5
{qq 	
varrr 
strrr 
=rr 
typerr 
.rr 

PrettyNamerr %
(rr% &
)rr& '
;rr' (
struu 
=uu 
struu 
.uu 
Replaceuu 
(uu 
$struu !
,uu! "
$struu# &
)uu& '
;uu' (
strvv 
=vv 
strvv 
.vv 
Replacevv 
(vv 
$strvv !
,vv! "
$strvv# &
)vv& '
;vv' (
strww 
=ww 
strww 
.ww 
Replaceww 
(ww 
$strww !
,ww! "
$strww# &
)ww& '
;ww' (
returnyy 
stryy 
;yy 
}zz 	
}{{ 
}|| �
_C:\Users\Климнюк\tring\App\Assets\Plugins\Zenject\Source\Editor\EditorStaticInstaller.cs
	namespace 	
Zenject
 
{ 
public 

abstract 
class !
EditorStaticInstaller /
</ 0
T0 1
>1 2
:3 4
InstallerBase5 B
where 
T 
: !
EditorStaticInstaller '
<' (
T( )
>) *
{ 
public 
static 
void 
Install "
(" #
)# $
{ 	
StaticContext 
. 
	Container #
.# $
Instantiate$ /
</ 0
T0 1
>1 2
(2 3
)3 4
.4 5
InstallBindings5 D
(D E
)E F
;F G
} 	
} 
} �5
bC:\Users\Климнюк\tring\App\Assets\Plugins\Zenject\Source\Editor\UnityInspectorListEditor.cs
	namespace 	
Zenject
 
{		 
public

 

abstract

 
class

 $
UnityInspectorListEditor

 2
:

3 4
Editor

5 ;
{ 
List 
< 
ReorderableList 
> 
_installersLists .
;. /
List 
< 
SerializedProperty 
>  !
_installersProperties! 6
;6 7
	protected 
abstract 
string !
[! "
]" # 
PropertyDisplayNames$ 8
{ 	
get 
; 
} 	
	protected 
abstract 
string !
[! "
]" #
PropertyNames$ 1
{ 	
get 
; 
} 	
	protected 
abstract 
string !
[! "
]" # 
PropertyDescriptions$ 8
{ 	
get 
; 
} 	
public 
virtual 
void 
OnEnable $
($ %
)% &
{ 	!
_installersProperties   !
=  " #
new  $ '
List  ( ,
<  , -
SerializedProperty  - ?
>  ? @
(  @ A
)  A B
;  B C
_installersLists!! 
=!! 
new!! "
List!!# '
<!!' (
ReorderableList!!( 7
>!!7 8
(!!8 9
)!!9 :
;!!: ;
var## 
descriptions## 
=##  
PropertyDescriptions## 3
;##3 4
var$$ 
names$$ 
=$$ 
PropertyNames$$ %
;$$% &
var%% 
displayNames%% 
=%%  
PropertyDisplayNames%% 3
;%%3 4
Assert'' 
.'' 
IsEqual'' 
('' 
descriptions'' '
.''' (
Length''( .
,''. /
names''0 5
.''5 6
Length''6 <
)''< =
;''= >
var)) 
infos)) 
=)) 

Enumerable)) "
.))" #
Range))# (
())( )
$num))) *
,))* +
names)), 1
.))1 2
Length))2 8
)))8 9
.))9 :
Select)): @
())@ A
i))A B
=>))C E
new))F I
{))J K
Name))L P
=))Q R
names))S X
[))X Y
i))Y Z
]))Z [
,))[ \
DisplayName))] h
=))i j
displayNames))k w
[))w x
i))x y
]))y z
,))z {
Description	))| �
=
))� �
descriptions
))� �
[
))� �
i
))� �
]
))� �
}
))� �
)
))� �
.
))� �
ToList
))� �
(
))� �
)
))� �
;
))� �
foreach++ 
(++ 
var++ 
info++ 
in++  
infos++! &
)++& '
{,, 
var-- 
installersProperty-- &
=--' (
serializedObject--) 9
.--9 :
FindProperty--: F
(--F G
info--G K
.--K L
Name--L P
)--P Q
;--Q R!
_installersProperties.. %
...% &
Add..& )
(..) *
installersProperty..* <
)..< =
;..= >
ReorderableList00 
installersList00  .
=00/ 0
new001 4
ReorderableList005 D
(00D E
serializedObject00E U
,00U V
installersProperty00W i
,00i j
true00k o
,00o p
true00q u
,00u v
true00w {
,00{ |
true	00} �
)
00� �
;
00� �
_installersLists11  
.11  !
Add11! $
(11$ %
installersList11% 3
)113 4
;114 5
var33 

closedName33 
=33  
info33! %
.33% &
DisplayName33& 1
;331 2
var44 

closedDesc44 
=44  
info44! %
.44% &
Description44& 1
;441 2
installersList66 
.66 
drawHeaderCallback66 1
+=662 4
rect665 9
=>66: <
{77 
GUI88 
.88 
Label88 
(88 
rect88 "
,88" #
new99 

GUIContent99 "
(99" #

closedName99# -
,99- .

closedDesc99/ 9
)999 :
)99: ;
;99; <
}:: 
;:: 
installersList;; 
.;; 
drawElementCallback;; 2
+=;;3 5
(;;6 7
rect;;7 ;
,;;; <
index;;= B
,;;B C
active;;D J
,;;J K
focused;;L S
);;S T
=>;;U W
{<< 
rect== 
.== 
width== 
-=== !
$num==" $
;==$ %
rect>> 
.>> 
x>> 
+=>> 
$num>>  
;>>  !
	EditorGUI?? 
.?? 
PropertyField?? +
(??+ ,
rect??, 0
,??0 1
installersProperty??2 D
.??D E"
GetArrayElementAtIndex??E [
(??[ \
index??\ a
)??a b
,??b c

GUIContent??d n
.??n o
none??o s
,??s t
true??u y
)??y z
;??z {
}@@ 
;@@ 
}AA 
}BB 	
publicDD 
sealedDD 
overrideDD 
voidDD #
OnInspectorGUIDD$ 2
(DD2 3
)DD3 4
{EE 	
serializedObjectFF 
.FF 
UpdateFF #
(FF# $
)FF$ %
;FF% &
OnGuiHH 
(HH 
)HH 
;HH 
serializedObjectJJ 
.JJ #
ApplyModifiedPropertiesJJ 4
(JJ4 5
)JJ5 6
;JJ6 7
}KK 	
	protectedMM 
virtualMM 
voidMM 
OnGuiMM $
(MM$ %
)MM% &
{NN 	
ifOO 
(OO 
ApplicationOO 
.OO 
	isPlayingOO %
)OO% &
{PP 
GUIQQ 
.QQ 
enabledQQ 
=QQ 
falseQQ #
;QQ# $
}RR 
foreachTT 
(TT 
varTT 
listTT 
inTT  
_installersListsTT! 1
)TT1 2
{UU 
listVV 
.VV 
DoLayoutListVV !
(VV! "
)VV" #
;VV# $
}WW 
GUIYY 
.YY 
enabledYY 
=YY 
trueYY 
;YY 
}ZZ 	
}[[ 
}\\ �K
jC:\Users\Климнюк\tring\App\Assets\Plugins\Zenject\Source\Editor\EditorWindow\ZenjectEditorWindow.cs
	namespace 	
Zenject
 
{ 
public 

abstract 
class 
ZenjectEditorWindow -
:. /
EditorWindow0 <
{		 
[

 	
Inject

	 
]

 
[ 	
NonSerialized	 
] 
Kernel 
_kernel 
; 
[ 	
Inject	 
] 
[ 	
NonSerialized	 
]  
GuiRenderableManager !
_guiRenderableManager 2
;2 3
[ 	
NonSerialized	 
] 
DiContainer 

_container 
; 
[ 	
NonSerialized	 
] 
	Exception 
_fatalError 
; 
[ 	
NonSerialized	 
] 
GUIStyle 
_errorTextStyle  
;  !
GUIStyle 
ErrorTextStyle 
{ 	
get 
{ 
if 
( 
_errorTextStyle #
==$ &
null' +
)+ ,
{   
_errorTextStyle!! #
=!!$ %
new!!& )
GUIStyle!!* 2
(!!2 3
GUI!!3 6
.!!6 7
skin!!7 ;
.!!; <
label!!< A
)!!A B
;!!B C
_errorTextStyle"" #
.""# $
fontSize""$ ,
=""- .
$num""/ 1
;""1 2
_errorTextStyle## #
.### $
normal##$ *
.##* +
	textColor##+ 4
=##5 6
Color##7 <
.##< =
red##= @
;##@ A
_errorTextStyle$$ #
.$$# $
wordWrap$$$ ,
=$$- .
true$$/ 3
;$$3 4
_errorTextStyle%% #
.%%# $
	alignment%%$ -
=%%. /

TextAnchor%%0 :
.%%: ;
MiddleCenter%%; G
;%%G H
}&& 
return(( 
_errorTextStyle(( &
;((& '
})) 
}** 	
	protected,, 
DiContainer,, 
	Container,, '
{-- 	
get.. 
{.. 
return.. 

_container.. #
;..# $
}..% &
}// 	
public11 
virtual11 
void11 
OnEnable11 $
(11$ %
)11% &
{22 	
if33 
(33 
_fatalError33 
!=33 
null33 #
)33# $
{44 
return55 
;55 
}66 

Initialize88 
(88 
)88 
;88 
}99 	
	protected;; 
virtual;; 
void;; 

Initialize;; )
(;;) *
);;* +
{<< 	
Assert== 
.== 
IsNull== 
(== 

_container== $
)==$ %
;==% &

_container?? 
=?? 
new?? 
DiContainer?? (
(??( )
new??) ,
[??, -
]??- .
{??/ 0
StaticContext??1 >
.??> ?
	Container??? H
}??I J
)??J K
;??K L

_containerBB 
.BB "
AssertOnNewGameObjectsBB -
=BB. /
trueBB0 4
;BB4 5$
ZenjectManagersInstallerDD $
.DD$ %
InstallDD% ,
(DD, -

_containerDD- 7
)DD7 8
;DD8 9

_containerFF 
.FF 
BindFF 
<FF 
KernelFF "
>FF" #
(FF# $
)FF$ %
.FF% &
AsSingleFF& .
(FF. /
)FF/ 0
;FF0 1

_containerGG 
.GG 
BindGG 
<GG  
GuiRenderableManagerGG 0
>GG0 1
(GG1 2
)GG2 3
.GG3 4
AsSingleGG4 <
(GG< =
)GG= >
;GG> ?

_containerHH 
.HH 
BindInstanceHH #
(HH# $
thisHH$ (
)HH( )
;HH) *
InstallBindingsJJ 
(JJ 
)JJ 
;JJ 

_containerLL 
.LL 
QueueForInjectLL %
(LL% &
thisLL& *
)LL* +
;LL+ ,

_containerMM 
.MM 
ResolveRootsMM #
(MM# $
)MM$ %
;MM% &
_kernelOO 
.OO 

InitializeOO 
(OO 
)OO  
;OO  !
}PP 	
publicRR 
virtualRR 
voidRR 
	OnDisableRR %
(RR% &
)RR& '
{SS 	
ifTT 
(TT 
_fatalErrorTT 
!=TT 
nullTT #
)TT# $
{UU 
returnVV 
;VV 
}WW 
_kernelYY 
.YY 
DisposeYY 
(YY 
)YY 
;YY 
}ZZ 	
public\\ 
virtual\\ 
void\\ 
Update\\ "
(\\" #
)\\# $
{]] 	
if^^ 
(^^ 
_fatalError^^ 
!=^^ 
null^^ #
)^^# $
{__ 
return`` 
;`` 
}aa 
trycc 
{dd 
_kernelee 
.ee 
Tickee 
(ee 
)ee 
;ee 
}ff 
catchgg 
(gg 
	Exceptiongg 
egg 
)gg 
{hh 
Logii 
.ii 
ErrorExceptionii "
(ii" #
eii# $
)ii$ %
;ii% &
_fatalErrorjj 
=jj 
ejj 
;jj  
}kk 
Repaintnn 
(nn 
)nn 
;nn 
}oo 	
publicqq 
virtualqq 
voidqq 
OnGUIqq !
(qq! "
)qq" #
{rr 	
ifss 
(ss 
_fatalErrorss 
!=ss 
nullss #
)ss# $
{tt 
varuu 

labelWidthuu 
=uu  
$numuu! $
;uu$ %
varvv 
labelHeightvv 
=vv  !
$numvv" %
;vv% &
GUIxx 
.xx 
Labelxx 
(xx 
newxx 
Rectxx "
(xx" #
Screenxx# )
.xx) *
widthxx* /
/xx0 1
$numxx2 3
-xx4 5

labelWidthxx6 @
/xxA B
$numxxC D
,xxD E
ScreenxxF L
.xxL M
heightxxM S
/xxT U
$numxxV W
-xxX Y
labelHeightxxZ e
/xxf g
$numxxh i
,xxi j

labelWidthxxk u
,xxu v
labelHeight	xxw �
)
xx� �
,
xx� �
$str
xx� �
,
xx� �
ErrorTextStyle
xx� �
)
xx� �
;
xx� �
varzz 
buttonWidthzz 
=zz  !
$numzz" %
;zz% &
var{{ 
buttonHeight{{  
={{! "
$num{{# %
;{{% &
var|| 
offset|| 
=|| 
new||  
Vector2||! (
(||( )
$num||) *
,||* +
$num||, /
)||/ 0
;||0 1
if~~ 
(~~ 
GUI~~ 
.~~ 
Button~~ 
(~~ 
new~~ "
Rect~~# '
(~~' (
Screen~~( .
.~~. /
width~~/ 4
/~~5 6
$num~~7 8
-~~9 :
buttonWidth~~; F
/~~G H
$num~~I J
+~~K L
offset~~M S
.~~S T
x~~T U
,~~U V
Screen~~W ]
.~~] ^
height~~^ d
/~~e f
$num~~g h
-~~i j
buttonHeight~~k w
/~~x y
$num~~z {
+~~| }
offset	~~~ �
.
~~� �
y
~~� �
,
~~� �
buttonWidth
~~� �
,
~~� �
buttonHeight
~~� �
)
~~� �
,
~~� �
$str
~~� �
)
~~� �
)
~~� �
{ 
ExecuteFullReload
�� %
(
��% &
)
��& '
;
��' (
}
�� 
}
�� 
else
�� 
{
�� 
try
�� 
{
�� 
if
�� 
(
�� #
_guiRenderableManager
�� -
!=
��. 0
null
��1 5
)
��5 6
{
�� #
_guiRenderableManager
�� -
.
��- .
OnGui
��. 3
(
��3 4
)
��4 5
;
��5 6
}
�� 
}
�� 
catch
�� 
(
�� 
	Exception
��  
e
��! "
)
��" #
{
�� 
Log
�� 
.
�� 
ErrorException
�� &
(
��& '
e
��' (
)
��( )
;
��) *
_fatalError
�� 
=
��  !
e
��" #
;
��# $
}
�� 
}
�� 
}
�� 	
	protected
�� 
virtual
�� 
void
�� 
ExecuteFullReload
�� 0
(
��0 1
)
��1 2
{
�� 	
_kernel
�� 
=
�� 
null
�� 
;
�� #
_guiRenderableManager
�� !
=
��" #
null
��$ (
;
��( )

_container
�� 
=
�� 
null
�� 
;
�� 
_fatalError
�� 
=
�� 
null
�� 
;
�� 

Initialize
�� 
(
�� 
)
�� 
;
�� 
}
�� 	
public
�� 
abstract
�� 
void
�� 
InstallBindings
�� ,
(
��, -
)
��- .
;
��. /
}
�� 
}�� �
dC:\Users\Климнюк\tring\App\Assets\Plugins\Zenject\Source\Editor\Editors\SceneContextEditor.cs
	namespace 	
Zenject
 
{ 
[ "
CanEditMultipleObjects 
] 
[ 
CustomEditor 
( 
typeof 
( 
SceneContext %
)% &
)& '
]' (
[		 
NoReflectionBaking		 
]		 
public

 

class

 
SceneContextEditor

 #
:

$ %!
RunnableContextEditor

& ;
{ 
SerializedProperty !
_contractNameProperty 0
;0 1
SerializedProperty  
_parentNamesProperty /
;/ 0
SerializedProperty 6
*_parentNewObjectsUnderSceneContextProperty E
;E F
public 
override 
void 
OnEnable %
(% &
)& '
{ 	
base 
. 
OnEnable 
( 
) 
; !
_contractNameProperty !
=" #
serializedObject$ 4
.4 5
FindProperty5 A
(A B
$strB R
)R S
;S T 
_parentNamesProperty  
=! "
serializedObject# 3
.3 4
FindProperty4 @
(@ A
$strA W
)W X
;X Y6
*_parentNewObjectsUnderSceneContextProperty 6
=7 8
serializedObject9 I
.I J
FindPropertyJ V
(V W
$strW {
){ |
;| }
} 	
	protected 
override 
void 
OnGui  %
(% &
)& '
{ 	
base 
. 
OnGui 
( 
) 
; 
EditorGUILayout 
. 
PropertyField )
() *!
_contractNameProperty* ?
,? @
trueA E
)E F
;F G
EditorGUILayout 
. 
PropertyField )
() * 
_parentNamesProperty* >
,> ?
true@ D
)D E
;E F
EditorGUILayout 
. 
PropertyField )
() *6
*_parentNewObjectsUnderSceneContextProperty* T
)T U
;U V
}   	
}!! 
}"" �
_C:\Users\Климнюк\tring\App\Assets\Plugins\Zenject\Source\Editor\Editors\ContextEditor.cs
	namespace 	
Zenject
 
{ 
[ 
NoReflectionBaking 
] 
public 

class 
ContextEditor 
:  $
UnityInspectorListEditor! 9
{ 
	protected 
override 
string !
[! "
]" #
PropertyNames$ 1
{		 	
get

 
{ 
return 
new 
string !
[! "
]" #
{ 
$str 1
,1 2
$str %
,% &
$str '
,' (
} 
; 
} 
} 	
	protected 
override 
string !
[! "
]" # 
PropertyDisplayNames$ 8
{ 	
get 
{ 
return 
new 
string !
[! "
]" #
{ 
$str 2
,2 3
$str %
,% &
$str '
,' (
} 
; 
} 
}   	
	protected"" 
override"" 
string"" !
[""! "
]""" # 
PropertyDescriptions""$ 8
{## 	
get$$ 
{%% 
return&& 
new&& 
string&& !
[&&! "
]&&" #
{'' 
$str(( c
,((c d
$str)) _
,))_ `
$str** P
,**P Q
}++ 
;++ 
},, 
}-- 	
}.. 
}// 