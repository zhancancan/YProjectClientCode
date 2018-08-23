
message
	name: 			表名（数据名）
	indexField : 	默认索引字段 主键与唯一索引 字段用","分割， 多个用”|“ 分割
	
field
	name:	[must] 	: ---
	取消//label//label: 	[must]	: 和protobuf 一样 [required,optional,repeated] 
	type:	[must] 	: 支持的数据类型
						-	基础类型: {string, int, float, bool} bool 用 "true" "false"表示
						-	特殊类型: 
								{								
									-lang : 多语言，本字段存储一个key可以指向 LangManager的数据.
											如果数据中此字段的值为null，默认值= {message.name}{message.defaultIndexField.value}.{field.name}.
											例如 :if (color.name==null )color.name=Color.123.name
											导出多语言表时可以用这个规则，统一一下
											
									-'type name':指向的数据类型: "Type_RewardGroup" 
										如果是反向查询的话，可以来自于多表 比如
											Type_Mission.nodes ="Type_MissionNodeKill,Type_MissionNode_Enhance...."
								}
							
	query:	[option]: 快速索引，如果field.query=="true" 会对此字段建立快速索引表，只能是string, int 类型的字段可以使用
	
	-- 下面一个标签是管理查找用的--
	
	searchDir:	[option]: 	可用值关联的方向，可以省缺，默认为 forward
							{
								- forward: 正向查找
								- backward: 逆向查找
							}
						
							
	targetField: [option]: 指向的数据表中的字段名称，eg: id
	selfField:	 [option]：逆向查找时的自身表中的字段名称:eg:id
	
				下面为repeat类型的关联查询，单一的就不用说了吧，
				逆向查找规则：  Type_RewardGroup group = new Type_RewardGroup(){id="0"}
								string [] ts = Type_Groups.defineData.field["rewards"];
								List<object> objects =new List<object>;
								foreach( var t in ts){
									objects .addRange(dataBase.getTable(t).where(unit=>unit.groupId==group.id).toArray();
								}								
								Type_RewardGroup.rewards= objects.toArray();
								
				正向查找规则： Type_RwardGroup group = new Type_RewardGroup() {unitList="0,1,2,3"}// 默认规则： 分割符号为','
								int [] ids = group.unitList.split(",");
								group.rewards=[ids.Length]
								for(int i =0;i<ids.Length;i++) group.rewards[i]= dataBase.getTable("Type_RewardUnit").find(ids[i]);
								
	
				


